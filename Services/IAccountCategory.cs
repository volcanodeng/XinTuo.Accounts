using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Services
{
    public interface IAccountCategory
    {
        List<AccountCategoryRecord> GetMainAccountCategory();

        List<AccountCategoryRecord> GetSubCategory(int cid);
    }
}