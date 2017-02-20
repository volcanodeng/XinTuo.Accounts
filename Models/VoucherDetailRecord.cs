using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace XinTuo.Accounts.Models
{
    public class VoucherDetailRecord 
    {
        public virtual VoucherRecord Voucher { get; set; }

        public virtual string Abstract { get; set; }

        public virtual AccountRecord Account { get; set; }

        public virtual string AccountCode { get; set; }

        public virtual string AccountName { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual decimal Price { get; set; }

        public virtual decimal Debit { get; set; }

        public virtual decimal Credit { get; set; }

    }
}