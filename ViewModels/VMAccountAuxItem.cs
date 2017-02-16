using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMAccountAuxItem
    {
        public int AccId
        {
            get; set;
        }

        public int? Custom
        {
            get;set;
        }

        public int? Suppliers
        {
            get;set;
        }

        public int? Employee
        {
            get;set;
        }

        public int? Project
        {
            get;set;
        }

        public int? Sector
        {
            get;set;
        }

        public int? Inventory
        {
            get;set;
        }
    }
}