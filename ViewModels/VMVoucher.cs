using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMVoucher
    {
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

    }
}