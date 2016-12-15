﻿using System.Linq;
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
        private readonly IRepository<AccountRecord> _account;
        private readonly IMapper _mapper;
        private readonly ICompany _company;
        private readonly IAuthenticationService _authService;

        public Account(IContentManager contentManager,
                       IRepository<AccountCategoryRecord> accCategory,
                       IRepository<AccountRecord> account,
                       IMapper mapper,
                       ICompany company,
                       IAuthenticationService authService)
        {
            _contentManager = contentManager;
            _accCategoryRepository = accCategory;
            _account = account;
            _mapper = mapper;
            _company = company;
            _authService = authService;
        }

        public AccountRecord GetAccount(int id)
        {
            return _account.Fetch(c => c.Id == id).FirstOrDefault();
        }

        public void SaveAccount(VMAccount account)
        {
            AccountRecord newAccount = new AccountRecord();

            newAccount = _mapper.Map<VMAccount,AccountRecord>(account,newAccount);
            newAccount.AccountCategoryRecord = _accCategoryRepository.Get(account.CateId);
            newAccount.CompanyRecord = _company.GetCurrentCompany().Record;
            newAccount.Creator = _authService.GetAuthenticatedUser().Id;
            newAccount.CreateTime = DateTime.Now;
            newAccount.Updater = _authService.GetAuthenticatedUser().Id;
            newAccount.UpdateTime = DateTime.Now;

            _account.Create(newAccount);

        }

    }
}