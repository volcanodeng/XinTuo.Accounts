using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace XinTuo.Accounts.Models
{
    public class AuxiliaryRecord : ContentPartRecord
    {
        public virtual AuxiliaryTypeRecord AuxiliaryTypeRecord { get; set; }

        public virtual string AuxCode { get; set; }

        public virtual string AuxName { get; set; }

        public virtual int AuxState { get; set; }

        public virtual CompanyRecord CompanyRecord { get; set; }

        public virtual int Creator { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}