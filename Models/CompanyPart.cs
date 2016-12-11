using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.Users.Models;

namespace XinTuo.Accounts.Models
{
    public class CompanyPart : ContentPart<CompanyRecord>
    {
        
        public string FullName
        {
            get
            {
                return Record.FullName;
            }
            set
            {
                Record.FullName = value;
            }
        }

        public string ShortName
        {
            get
            {
                return Record.ShortName;
            }

            set
            {
                Record.ShortName = value;
            }
        }

        public RegionRecord Region
        {
            get
            {
                return Record.RegionRecord;
            }

            set
            {
                Record.RegionRecord = value;
            }
        }

        public string Address
        {
            get
            {
                return Record.Address;
            }
            set
            {
                Record.Address = value;
            }
        }

        public string Tel
        {
            get
            {
                return Record.Tel;
            }
            set
            {
                Record.Tel = value;
            }
        }
        public string ContractName
        {
            get
            {
                return Record.ContactName;
            }
            set
            {
                Record.ContactName = value;
            }
        }
        public string ContractMobile
        {
            get
            {
                return Record.ContactMobile;
            }
            set
            {
                Record.ContactMobile = value;
            }
        }
        public string ContractEmail
        {
            get
            {
                return Record.ContactEmail;
            }
            set
            {
                Record.ContactEmail = value;
            }
        }

        public int? StartYear
        {
            get
            {
                return Record.StartYear;
            }
            set
            {
                Record.StartYear = value;
            }
        }

        public int? StartPeriod
        {
            get
            {
                return Record.StartPeriod;
            }
            set
            {
                Record.StartPeriod = value;
            }
        }

        public string FiscalSystem
        {
            get
            {
                return Record.FiscalSystem;
            }

            set
            {
                Record.FiscalSystem = value;
            }
        }

        public IEnumerable<UserPartRecord> UserPart
        {
            get
            {
                return Record.Users.Select(u=>u.UserPartRecord);
            }
        }
    }
}