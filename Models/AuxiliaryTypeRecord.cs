using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.Models
{
    public class AuxiliaryTypeRecord
    {
        public virtual int Id { get; set; }

        public virtual string AuxType { get; set; }

        public CompanyRecord CompanyRecord { get; set; }

    }
}