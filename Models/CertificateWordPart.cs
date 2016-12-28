using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Models
{
    public class CertificateWordPart : ContentPart<CertificateWordRecord>
    {
        public string CertWord
        {
            get
            {
                return Record.CertWord;
            }
            set
            {
                Record.CertWord = value;
            }
        }

        public string PrintTitle
        {
            get
            {
                return Record.PrintTitle;
            }
            set
            {
                Record.PrintTitle = value;
            }
        }

        public int SortIndex
        {
            get
            {
                return Record.SortIndex;
            }
            set
            {
                Record.SortIndex = value;
            }
        }

        public int IsDefault
        {
            get
            {
                return Record.IsDefault;
            }
            set
            {
                Record.IsDefault = value;
            }
        }
        public CompanyRecord CompanyRecord
        {
            get
            {
                return Record.CompanyRecord;
            }
            set
            {
                Record.CompanyRecord = value;
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
    }
}