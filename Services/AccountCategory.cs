using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Services
{
    public class AccountCategory : IAccountCategory
    {
        private readonly IRepository<AccountCategoryRecord> _accCategory;

        public AccountCategory(IRepository<AccountCategoryRecord> accCategory)
        {
            _accCategory = accCategory;
        }

        public List<AccountCategoryRecord> GetMainAccountCategory()
        {
            return _accCategory.Fetch(c => !c.ParentAcId.HasValue).ToList();
        }

        public List<AccountCategoryRecord> GetSubCategory(int cid)
        {
            return _accCategory.Fetch(c => c.ParentAcId.HasValue && c.ParentAcId.Value == cid).ToList();
        }
    }
}