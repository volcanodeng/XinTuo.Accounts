using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XinTuo.Accounts.ViewModels
{
    public class VMCertWord
    {
        public int Id
        {
            get;set;
        }

        [Required,Display(Name ="凭证字")]
        public string CertWord
        {
            get;set;
        }

        [Display(Name ="打印标题")]
        public string PrintTitle
        {
            get;set;
        }

        public int SortIndex
        {
            get;set;
        }

        public bool IsDefault
        {
            get;set;
        }


    }
}