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

        public AccountCategoryRecord AccountCategory
        {
            get
            {
                return Record.AccountCategory;
            }
            set
            {
                Record.AccountCategory = value;
            }
        }
    }
}