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

        public Auxiliary(IRepository<AuxiliaryTypeRecord> auxType,IContentManager contentManager)
        {
            _auxType = auxType;
            _contentManager = contentManager;
        }

        public List<AuxiliaryTypeRecord> GetBaseAuxType()
        {
            return _auxType.Table.ToList();
        }

        
    }
}