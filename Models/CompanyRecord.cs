﻿using System;
using System.Collections.Generic;
using Orchard.ContentManagement.Records;
using Orchard.Users.Models;

namespace XinTuo.Accounts.Models
{
    public class CompanyRecord : ContentPartRecord
    {
        public virtual int CId { get; set; }
        public virtual string FullName { get; set; }
        public virtual string ShortName { get; set; }
        public virtual RegionRecord Region { get; set; } 
        public virtual string Address { get; set; }
        public virtual string Tel { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string ContactMobile { get; set; }
        public virtual string ContactEmail { get; set; }
    }
}