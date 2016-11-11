﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace XinTuo.Accounts.Models
{
    public class AccountRecord : ContentPartRecord
    {
        public virtual string AccCode { get; set; }

        public virtual string ParentCode { get; set; }

        public virtual AccountCategoryRecord AccountCategory { get; set; }

        public virtual string AccName { get; set; }

        public virtual string Direction { get; set; }

        public virtual string AuxIds { get; set; }

        public virtual string AuxCodes { get; set; }

        public virtual string AuxNames { get; set; }

        public virtual string Unit { get; set; }

        public virtual decimal InitialQuantity { get; set; }

        public virtual decimal InitialBalance { get; set; }

        public virtual decimal YtdDebitQuantity { get; set; }

        public virtual decimal YtdDebit { get; set; }

        public virtual decimal YtdCreditQuantity { get; set; }

        public virtual decimal YtdCredit { get; set; }

        public virtual decimal YtdBeginBalanceQuantity { get; set; }

        public virtual decimal YtdBeginBalance { get; set; }

        public virtual int AccState { get; set; }

        public virtual CompanyRecord Company { get; set; }

        public virtual int Creator { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual int Updater { get; set; }

        public virtual DateTime UpdateTime { get; set; }
    }
}