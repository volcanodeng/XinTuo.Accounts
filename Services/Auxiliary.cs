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

        public AuxiliaryTypeRecord SaveAuxType(AuxiliaryTypeRecord customType)
        {
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

        public AuxiliaryRecord SaveAuxiliary(VMAuxiliary aux)
        {
            AuxiliaryRecord newAux = _mapper.Map<VMAuxiliary, AuxiliaryRecord>(aux);
            newAux.AuxiliaryType = _auxType.Get(aux.AuxTypeId);
            newAux.CreateTime = DateTime.Now;

            IUser user = _authService.GetAuthenticatedUser();
            if (user != null) newAux.Creator = user.Id;

            CompanyPart cp = _company.GetCurrentCompany();
            if (cp != null)
            {
                newAux.Company = cp.Record;
            }
            else
            {
                return null;
            }

            _auxiliary.Create(newAux);
            return newAux;
        }
        
    }
}