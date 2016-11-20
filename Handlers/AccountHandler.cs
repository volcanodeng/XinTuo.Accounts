using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Handlers
{
    public class AccountHandler : ContentHandler
    {
        public AccountHandler(IRepository<AccountRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<AccountPart>("Account"));
        }
    }
}