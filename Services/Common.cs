using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.Services
{
    public static class Common
    {
        public static string GetAccountsCacheName(int comId)
        {
            return string.Format("AccountsOfCompany{0}", comId);
        }

        public static string GetVoucherCacheName(int comId)
        {
            return string.Format("VoucherOfCompany{0}", comId);
        }

    }
}