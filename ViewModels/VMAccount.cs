using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMAccount
    {
        public string AccCode
        {
            get;set;
        }

        public string ParentCode
        {
            get;set;
        }

        public string CateId
        {
            get;set;
        }

        public string AccName
        {
            get;set;
        }

        public string Direction
        {
            get;set;
        }

        public string AuxCodes
        {
            get;set;
        }

        public string AuxNames
        {
            get;set;
        }

        public string Unit
        {
            get;set;
        }

        public decimal InitialQuantity
        {
            get;set;
        }

        public decimal InitialBalance
        {
            get;set;
        }

        public decimal YtdDebitQuantity
        {
            get;set;
        }

        public decimal YtdDebit
        {
            get;set;
        }

        public decimal YtdCreditQuantity
        {
            get;set;
        }

        public decimal YtdCredit
        {
            get;set;
        }

        public decimal YtdBeginBalanceQuantity
        {
            get;set;
        }

        public decimal YtdBeginBalance
        {
            get;set;
        }

        public int AccState
        {
            get;set;
        }

        public int CompanyId
        {
            get;set;
        }

        public string CompanyName
        {
            get;set;
        }

        public string CreatorName
        {
            get;
            set;
        }

        public DateTime CreateTime
        {
            get;
            set;
        }

    }
}