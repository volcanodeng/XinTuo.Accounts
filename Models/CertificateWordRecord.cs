﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace XinTuo.Accounts.Models
{
    public class CertificateWordRecord : ContentPartRecord
    {
        public virtual string CertWord { get; set; }

        public virtual string PrintTitle { get; set; }

        public virtual int SortIndex { get; set; }

        public virtual bool IsDefault { get; set; }

        public virtual CompanyRecord Company { get; set; }

        public virtual int Creator { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}