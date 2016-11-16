using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using Orchard;

namespace XinTuo.Accounts.Services
{
    public interface IAccountCategory : IDependency
    {
        List<AccountCategoryRecord> GetMainAccountCategory();

        List<AccountCategoryRecord> GetSubCategory(int cid);
    }
}