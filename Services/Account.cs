using System.Linq;
using XinTuo.Accounts.Models;
using Orchard.Data;
using Orchard.ContentManagement;
using Orchard.Security;
using XinTuo.Accounts.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using Orchard.Caching.Services;
using NHibernate;

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
        private readonly ICacheService _cache;

        public Account(IContentManager contentManager,
                       IRepository<AccountCategoryRecord> accCategory,
                       IRepository<AccountRecord> account,
                       IMapper mapper,
                       ICompany company,
                       IAuthenticationService authService,
                       ICacheService cache)
        {
            _contentManager = contentManager;
            _accCategoryRepository = accCategory;
            _account = account;
            _mapper = mapper;
            _company = company;
            _authService = authService;
            _cache = cache;
        }

        private List<AccountRecord> GetAccountsOfCompany()
        {
            int comId = _company.GetCurrentCompanyId();
            List<AccountRecord> accounts = _cache.Get<List<AccountRecord>>(Common.GetAccountsCacheName(comId));

            if (accounts == null || accounts.Count == 0)
            {
                accounts = _account.Fetch(a => a.CompanyRecord.Id == comId).ToList();

                _cache.Put<List<AccountRecord>>(Common.GetAccountsCacheName(comId), accounts);
            }
            return accounts;
        }

        public AccountRecord GetAccount(int id)
        {
            return this.GetAccountsOfCompany().Where(a => a.Id == id).FirstOrDefault();
        }

        public List<AccountRecord> GetAccounts(int cateId)
        {
            return this.GetAccountsOfCompany().Where(a => a.AccountCategoryRecord.Id == cateId || a.AccountCategoryRecord.ParentAcId == cateId).OrderBy(a=>a.AccCode).ToList();
        }

        public List<VMAccount> GetVMAccounts(int cateId)
        {
            var accounts = GetAccounts(cateId);
            return _mapper.Map<List<AccountRecord>, List<VMAccount>>(accounts);
        }

        public void SaveAccount(VMAccount account)
        {
            if (account.AccId <= 0)
            {
                AccountRecord newAccount = new AccountRecord();

                newAccount = _mapper.Map<VMAccount, AccountRecord>(account, newAccount);
                newAccount.AccountCategoryRecord = _accCategoryRepository.Get(account.CateId);
                newAccount.CompanyRecord = _company.GetCurrentCompany().Record;
                newAccount.Creator = _authService.GetAuthenticatedUser().Id;
                newAccount.CreateTime = DateTime.Now;
                newAccount.Updater = _authService.GetAuthenticatedUser().Id;
                newAccount.UpdateTime = DateTime.Now;
                newAccount.AccState = 1;

                _account.Create(newAccount);
            }
            else
            {
                AccountRecord acc = _account.Get(account.AccId);
                if(acc != null)
                {
                    acc.AccCode = account.AccCode;
                    acc.AccName = account.AccName;
                    acc.Direction = account.Direction;
                    acc.IsAuxiliary = (!string.IsNullOrEmpty(account.IsAuxiliary) && account.IsAuxiliary.ToLower() == "on") ? 1 : 0;
                    acc.AuxTypeIds = account.AuxTypeIds;
                    acc.AuxTypeNames = account.AuxTypeNames;
                    acc.IsQuantity = (!string.IsNullOrEmpty(account.IsQuantity) && account.IsQuantity.ToLower() == "on") ? 1 : 0;
                    acc.Unit = account.Unit;
                }
                _account.Update(acc);
            }

            _cache.Remove(Common.GetAccountsCacheName(_company.GetCurrentCompanyId()));
        }

    }
}