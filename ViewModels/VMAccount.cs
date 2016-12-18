using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XinTuo.Accounts.ViewModels
{
    public class VMAccount
    {
        public int AccId
        {
            get;set;
        }

        public string AccCode
        {
            get;set;
        }

        public string ParentCode
        {
            get;set;
        }

        public int CateId
        {
            get;set;
        }

        public string cateName
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

        public string IsAuxiliary { get; set; }

        public string AuxTypeIds { get; set; }

        public string IsQuantity { get; set; }

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

    }
}