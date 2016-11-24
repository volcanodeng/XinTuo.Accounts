using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMAuxiliary
    {
        public int Id
        {
            get;set;
        }

        public string AuxCode
        {
            get;set;
        }

        public string AuxName
        {
            get;set;
        }

        public int AuxTypeId
        {
            get;set;
        }

        public int AuxState
        {
            get;set;
        }
    }
}