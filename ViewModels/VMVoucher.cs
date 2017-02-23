using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMVoucher
    {
        public int VId { get; set; }

        public int CertWordSN { get; set; }

        public string CertWord { get; set; }

        public DateTime Date { get; set; }

        public int InvoiceCount { get; set; }

        public int State { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Review { get; set; }

        public DateTime ReviewTime { get; set; }

        public List<VMVoucherDetail> VoucherDetails { get; set; }
    }

    public class VMVoucherDetail
    {
        public string Abstract { get; set; }

        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string AccountCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }
    }
}