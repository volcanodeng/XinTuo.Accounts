using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using Orchard.Data;
using Orchard.Security;
using Orchard.Services;
using Orchard.ContentManagement;
using XinTuo.Accounts.ViewModels;

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

        public AccountPart GetAccount(int id)
        {
            return _contentManager.Query<AccountPart, AccountRecord>().Where(c => c.Id == id).List().FirstOrDefault();
        }

        public AccountPart SaveAccount(VMAccount account)
        {
            return _contentManager.New("Account").As<AccountPart>();
        }
    }
}