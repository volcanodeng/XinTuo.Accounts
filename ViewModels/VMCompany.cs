using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMCompany
    {
        public string FullName
        {
            get;set;
        }

        public string ShortName
        {
            get;set;
        }

        public int RegionId
        {
            get;set;
        }

        public string RegionName
        {
            get;set;
        }

        public string Address
        {
            get;set;
        }

        public string Tel
        {
            get;set;
        }

        public string ContractName
        {
            get;set;
        }

        public string ContractMobile
        {
            get;set;
        }

        public string ContractEmail
        {
            get;set;
        }

        public List<VMUser> Users
        {
            get;set;
        }
    }
}