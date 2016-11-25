using System.ComponentModel.DataAnnotations;

namespace XinTuo.Accounts.ViewModels
{
    public class VMAuxiliary
    {
        public int? AuxId
        {
            get;set;
        }

        [Required,Display(Name ="编码")]
        public string AuxCode
        {
            get;set;
        }

        [Required, Display(Name = "名称")]
        public string AuxName
        {
            get;set;
        }

        [Display(Name ="类型")]
        public int AuxTypeId
        {
            get;set;
        }

        [Display(Name = "状态")]
        public int AuxState
        {
            get;set;
        }
    }
}