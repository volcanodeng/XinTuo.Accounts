using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using Orchard.Data;
using Orchard.Security;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Services
{
    public class Account : IAccountcs
    {
        private readonly IContentManager _contentManager;
        private readonly IRepository<AccountRecord> _accountRecord;

        public Account(IContentManager contentManager,IRepository<AccountRecord> accountRecord)
        {
            _contentManager = contentManager;
            _accountRecord = accountRecord;
        }
    }
}