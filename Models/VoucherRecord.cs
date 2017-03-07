using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace XinTuo.Accounts.Models
{
    public class VoucherRecord 
    {
        public virtual int Id { get; set; }

        public virtual CertificateWordRecord CertificateWord { get; set; }

        public virtual int CertWordSN { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int PaymentTerms { get; set; }

        public virtual int InvoiceCount { get; set; }

        public virtual int State { get; set; }

        public virtual CompanyRecord CompanyRecord { get; set; }

        public virtual int Creator { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual int? Review { get; set; }

        public virtual DateTime? ReviewTime { get; set; }

        //public virtual ICollection<VoucherDetailRecord> VoucherDetailRecords { get; set; }
    }
}