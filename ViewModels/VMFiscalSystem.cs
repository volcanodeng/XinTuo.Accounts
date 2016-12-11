using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMFiscalSystem
    {
        public int? Year
        {
            get;set;
        }

        public int? Period
        {
            get;set;
        }

        public string Fiscal
        {
            get;set;
        }
    }
}