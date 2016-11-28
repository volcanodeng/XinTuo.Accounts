using System.Linq;
using XinTuo.Accounts.Models;
using Orchard.Data;
using Orchard.ContentManagement;
using Orchard.Security;
using XinTuo.Accounts.ViewModels;
using AutoMapper;
using System;

namespace XinTuo.Accounts.Services
{
    public class Account : IAccount
    {
        private readonly IContentManager _contentManager;
        private readonly IRepository<AccountCategoryRecord> _accCategoryRepository;
        private readonly IMapper _mapper;
        private readonly ICompany _company;
        private readonly IAuthenticationService _authService;

        public Account(IContentManager contentManager,
                       IRepository<AccountCategoryRecord> accCategory,
                       IMapper mapper,
                       ICompany company,
                       IAuthenticationService authService)
        {
            _contentManager = contentManager;
            _accCategoryRepository = accCategory;
            _mapper = mapper;
            _company = company;
            _authService = authService;
        }

        public AccountPart GetAccount(int id)
        {
            return _contentManager.Query<AccountPart, AccountRecord>().Where(c => c.Id == id).List().FirstOrDefault();
        }

        public AccountPart SaveAccount(VMAccount account)
        {
            AccountPart newAccount = _contentManager.New("Account").As<AccountPart>();

            newAccount = _mapper.Map<VMAccount,AccountPart>(account,newAccount);
            newAccount.AccountCategory = _accCategoryRepository.Get(account.CateId);
            newAccount.Company = _company.GetCurrentCompany().Record;
            newAccount.Creator = _authService.GetAuthenticatedUser().Id;
            newAccount.CreateTime = DateTime.Now;
            newAccount.Updater = _authService.GetAuthenticatedUser().Id;
            newAccount.UpdateTime = DateTime.Now;

            return newAccount;
        }

    }
}