using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XinTuo.Accounts.ViewModels
{
    public class VMCompany
    {
        [Required,Display(Name = "公司全称")]
        public string FullName
        {
            get;set;
        }

        [Display(Name = "公司简称")]
        public string ShortName
        {
            get;set;
        }

        public int RegionId
        {
            get;set;
        }

        [Display(Name = "行政区划")]
        public string RegionName
        {
            get;set;
        }

        [Display(Name = "公司地址")]
        public string Address
        {
            get;set;
        }

        [Display(Name = "公司电话")]
        public string Tel
        {
            get;set;
        }

        [Required, Display(Name = "联系人姓名")]
        public string ContractName
        {
            get;set;
        }

        [Required, Display(Name = "联系人电话")]
        public string ContractMobile
        {
            get;set;
        }

        [Required, Display(Name = "联系人邮箱")]
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