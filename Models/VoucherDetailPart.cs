using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Models
{
    public class VoucherDetailPart : ContentPart<VoucherDetailRecord>
    {
        public VoucherRecord Voucher
        {
            get
            {
                return Record.Voucher;
            }
            set
            {
                Record.Voucher = value;
            }

        }

        public string Abstract
        {
            get
            {
                return Record.Abstract;
            }
            set
            {
                Record.Abstract = value;
            }
        }

        public AccountRecord Account

        {
            get
            {
                return Record.Account;
            }
            set
            {
                Record.Account = value;
            }
        }

        public string AccountCode
        {
            get
            {
                return Record.AccountCode;
            }
            set
            {
                Record.AccountCode = value;
            }
        }

        public string AccountName
        {
            get
            {
                return Record.AccountName;
            }
            set
            {
                Record.AccountName = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return Record.Quantity;
            }
            set
            {
                Record.Quantity = value;
            }
        }

        public decimal Price
        {
            get
            {
                return Record.Price;
            }
            set
            {
                Record.Price = value;
            }
        }

        public decimal Debit

        {
            get
            {
                return Record.Debit;
            }
            set
            {
                Record.Debit = value;
            }
        }

        public decimal Credit
        {
            get
            {
                return Record.Credit;
            }
            set
            {
                Record.Credit = value;
            }
        }

    }
}