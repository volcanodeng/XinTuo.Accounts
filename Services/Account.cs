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
        private readonly IRepository<AccountCategoryRecord> _accCategoryRepository;

        public Account(IContentManager contentManager,IRepository<AccountCategoryRecord> accCategory)
        {
            _contentManager = contentManager;
            _accCategoryRepository = accCategory;
        }

        public AccountPart GetAccount(int id)
        {
            return _contentManager.Query<AccountPart, AccountRecord>().Where(c => c.Id == id).List().FirstOrDefault();
        }

        public AccountPart SaveAccount(VMAccount account)
        {
            AccountPart newAccount = _contentManager.New("Account").As<AccountPart>();
            newAccount.AccCode = account.AccCode;
            newAccount.ParentCode = account.ParentCode;
            newAccount.AccountCategory = _accCategoryRepository.Get(account.CateId);


            return newAccount;
        }
    }
}