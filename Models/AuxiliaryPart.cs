using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Models
{
    public class AuxiliaryPart : ContentPart<AuxiliaryRecord>
    {
        public AuxiliaryTypeRecord AuxiliaryType
        {
            get
            {
                return Record.AuxiliaryType;
            }
            set
            {
                Record.AuxiliaryType = value;
            }
        }

        public string AuxCode
        {
            get
            {
                return Record.AuxCode;
            }
            set
            {
                Record.AuxCode = value;
            }
        }

        public string AuxName
        {
            get
            {
                return Record.AuxName;
            }
            set
            {
                Record.AuxName = value;
            }
        }

        public int AuxState
        {
            get
            {
                return Record.AuxState;
            }
            set
            {
                Record.AuxState = value;
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
    }
}