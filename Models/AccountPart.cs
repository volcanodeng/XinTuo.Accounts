using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Models
{
    public class AccountPart : ContentPart<AccountRecord>
    {
        public string AccCode
        {
            get { return Record.AccCode; }
            set { Record.AccCode = value; }
        }

        public string ParentCode
        {
            get { return Record.ParentCode; }
            set { Record.ParentCode = value; }
        }

        public AccountCategoryRecord AccountCategoryRecord
        {
            get
            {
                return Record.AccountCategoryRecord;
            }
            set
            {
                Record.AccountCategoryRecord = value;
            }
        }

        public string AccName
        {
            get { return Record.AccName; }
            set { Record.AccName = value; }
        }

        public string Direction
        {
            get { return Record.Direction; }
            set { Record.Direction = value; }
        }

        public string AuxIds
        {
            get { return Record.AuxIds; }
            set { Record.AuxIds = value; }
        }

        public string AuxCodes
        {
            get { return Record.AuxCodes; }
            set { Record.AuxCodes = value; }
        }

        public string AuxNames
        {
            get { return Record.AuxNames; }
            set { Record.AuxNames = value; }
        }

        public string Unit
        {
            get { return Record.Unit; }
            set { Record.Unit = value; }
        }

        public decimal InitialQuantity
        {
            get
            {
                return Record.InitialQuantity;
            }
            set { Record.InitialQuantity = value; }
        }

        public decimal InitialBalance
        {
            get { return Record.InitialBalance; }
            set { Record.InitialBalance = value; }
        }

        public decimal YtdDebitQuantity
        {
            get { return Record.YtdDebitQuantity; }

            set { Record.YtdDebitQuantity = value; }
        }

        public decimal YtdDebit
        {
            get { return Record.YtdDebit; }
            set { Record.YtdDebit = value; }
        }

        public decimal YtdCreditQuantity
        {
            get { return Record.YtdCreditQuantity; }
            set { Record.YtdCreditQuantity = value; }
        }

        public decimal YtdCredit
        {
            get { return Record.YtdCredit; }
            set { Record.YtdCredit = value; }
        }

        public decimal YtdBeginBalanceQuantity
        {
            get { return Record.YtdBeginBalanceQuantity; }
            set { Record.YtdBeginBalanceQuantity = value; }
        }

        public decimal YtdBeginBalance
        {
            get { return Record.YtdBeginBalance; }
            set { Record.YtdBeginBalance = value; }
        }

        public int AccState
        {
            get { return Record.AccState; }
            set { Record.AccState = value; }
        }

        public CompanyRecord CompanyRecord
        {
            get { return Record.CompanyRecord; }
            set { Record.CompanyRecord = value; }
        }

        public int Creator
        {
            get { return Record.Creator; }
            set { Record.Creator = value; }
        }

        public DateTime CreateTime
        {
            get { return Record.CreateTime; }
            set { Record.CreateTime = value; }
        }

        public int Updater
        {
            get { return Record.Updater; }
            set { Record.Updater = value; }
        }

        public DateTime UpdateTime
        {
            get { return Record.UpdateTime; }
            set
            {
                Record.UpdateTime = value;
            }
        }
    }
}