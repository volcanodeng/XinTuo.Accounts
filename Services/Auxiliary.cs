using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data;
using Orchard.ContentManagement;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Services
{
    public class Auxiliary : IAuxiliary
    {
        private readonly IRepository<AuxiliaryTypeRecord> _auxType;
        private readonly IContentManager _contentManager;
        private readonly ICompany _company;

        public Auxiliary(IRepository<AuxiliaryTypeRecord> auxType,IContentManager contentManager,ICompany company)
        {
            _auxType = auxType;
            _contentManager = contentManager;
            _company = company;
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
        
    }
}