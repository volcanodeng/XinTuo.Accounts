using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Models
{
    public class VoucherPart : ContentPart<VoucherRecord>
    {
        public CertificateWordRecord CertificateWord
        {
            get
            {
                return Record.CertificateWord;
            }
            set
            {
                Record.CertificateWord = value;
            }
        }

        public int CertWordSN
        {
            get
            {
                return Record.CertWordSN;
            }
            set
            {
                Record.CertWordSN = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return Record.Date;
            }
            set
            {
                Record.Date = value;
            }
        }

        public int InvoiceCount
        {
            get
            {
                return Record.InvoiceCount;
            }
            set
            {
                Record.InvoiceCount = value;
            }
        }

        public int State
        {
            get
            {
                return Record.State;
            }
            set
            {
                Record.State = value;
            }
        }

        public CompanyRecord Company
        {
            get
            {
                return Record.Company;
            }
            set
            {
                Record.Company = value;
            }
        }

        public int Creator
        {
            get
            {
                return Record.Creator;
            }
            set
            {
                Record.Creator = value;
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return Record.CreateTime;
            }
            set
            {
                Record.CreateTime = value;
            }
        }
        public int Review
        {
            get
            {
                return Record.Review;
            }
            set
            {
                Record.Review = value;
            }
        }

        public DateTime ReviewTime
        {
            get
            {
                return Record.ReviewTime;
            }
            set
            {
                Record.ReviewTime = value;
            }
        }
    }
}