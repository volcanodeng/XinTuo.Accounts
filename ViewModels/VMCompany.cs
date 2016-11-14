using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XinTuo.Accounts.ViewModels
{
    public class VMCompany
    {
        public int Id { get; set; }

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

        public int CityId { get; set; }

        public int ProvinceId { get; set; }

        public string CityName { get; set; }

        public string ProvinceName { get; set; }

        [Display(Name = "行政区划")]
        public string CountyName
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