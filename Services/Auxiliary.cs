using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data;
using Orchard.ContentManagement;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using AutoMapper;
using Orchard.Security;

namespace XinTuo.Accounts.Services
{
    public class Auxiliary : IAuxiliary
    {
        private readonly IRepository<AuxiliaryTypeRecord> _auxType;
        private readonly IRepository<AuxiliaryRecord> _auxiliary;
        private readonly IContentManager _contentManager;
        private readonly ICompany _company;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authService;

        public Auxiliary(IRepository<AuxiliaryTypeRecord> auxType,
                         IContentManager contentManager,
                         ICompany company,
                         IRepository<AuxiliaryRecord> auxiliary,
                         IMapper mapper,
                         IAuthenticationService authService)
        {
            _auxType = auxType;
            _auxiliary = auxiliary;
            _contentManager = contentManager;
            _company = company;
            _mapper = mapper;
            _authService = authService;
        }

        public List<AuxiliaryTypeRecord> GetBaseAuxType()
        {
            return _auxType.Table.ToList();
        }

        public AuxiliaryTypeRecord SaveAuxType(string auxTypeName)
        {
            AuxiliaryTypeRecord customType = new AuxiliaryTypeRecord();
            customType.AuxType = auxTypeName;

            CompanyPart cp = _company.GetCurrentCompany();
            if(cp != null)
            {
                customType.CompanyRecord = cp.Record;
            }
            else
            {
                return null;
            }

            _auxType.Create(customType);

            return customType;
        }

        public AuxiliaryPart SaveAuxiliary(VMAuxiliary aux)
        {
            AuxiliaryPart newAux = null;
            if (!aux.AuxId.HasValue || aux.AuxId <= 0)
            {
                newAux = _contentManager.New<AuxiliaryPart>("Auxiliary");

                newAux = _mapper.Map<VMAuxiliary, AuxiliaryPart>(aux, newAux);
                newAux.AuxiliaryType = _auxType.Get(aux.AuxTypeId);
                newAux.CreateTime = DateTime.Now;

                IUser user = _authService.GetAuthenticatedUser();
                if (user != null) newAux.Creator = user.Id;

                CompanyPart cp = _company.GetCurrentCompany();
                if (cp != null) newAux.Company = cp.Record;

                _contentManager.Create(newAux);
            }
            else
            {
                newAux = _contentManager.Get<AuxiliaryPart>(aux.AuxId.Value);
                if (newAux != null)
                {
                    newAux = _mapper.Map<VMAuxiliary, AuxiliaryPart>(aux, newAux);
                    _contentManager.Restore(newAux.ContentItem, VersionOptions.Latest);
                }
            }
            return newAux;
        }

        public void DeleteAuxiliary(int auxId)
        {
            var aux = _contentManager.Get<AuxiliaryPart>(auxId);
            if (aux != null) _contentManager.Remove(aux.ContentItem);
        }

        public IEnumerable<AuxiliaryPart> GetAuxiliaries(int companyId,int auxTypeId)
        {
            return _contentManager.Query<AuxiliaryPart, AuxiliaryRecord>().Where(a=>a.CompanyRecord.Id== companyId && a.AuxiliaryTypeRecord.Id == auxTypeId).List();
        }

        public List<VMAuxiliary> GetAuxiliaryForCom(int auxTypeId)
        {
            var curCom = _company.GetCurrentCompany();
            var auxList = GetAuxiliaries(curCom.Id, auxTypeId).ToList();

            return _mapper.Map<List<AuxiliaryPart>, List<VMAuxiliary>>(auxList);
        }

        public VMAuxiliary GetAuxiliary(int auxId)
        {
            IEnumerable<AuxiliaryPart> auxs = _contentManager.Query<AuxiliaryPart, AuxiliaryRecord>().Where(a => a.CompanyRecord.Id == _company.GetCurrentCompanyId() && a.Id == auxId).List();
            return _mapper.Map<AuxiliaryPart, VMAuxiliary>(auxs.FirstOrDefault());
        }

    }
}