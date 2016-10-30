using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace XinTuo.Accounts.Models
{
    public class AbstractRecord : ContentPartRecord
    {
        public virtual string Abstract { get; set; }

        public virtual int Creator { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}