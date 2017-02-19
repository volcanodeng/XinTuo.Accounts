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
        private readonly IAccountCategory _accCate;
        private readonly IAuxiliary _auxiliary;

        public Account(IContentManager contentManager,
                       IRepository<AccountCategoryRecord> accCategory,
                       IRepository<AccountRecord> account,
                       IMapper mapper,
                       ICompany company,
                       IAuthenticationService authService,
                       ICacheService cache,
                       IAccountCategory accCate,
                       IAuxiliary auxiliary)
        {
            _contentManager = contentManager;
            _accCategoryRepository = accCategory;
            _account = account;
            _mapper = mapper;
            _company = company;
            _authService = authService;
            _cache = cache;
            _accCate = accCate;
            _auxiliary = auxiliary;
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

        private void ClearAccountsCache()
        {
            int comId = _company.GetCurrentCompanyId();
            _cache.Remove(Common.GetAccountsCacheName(comId));
        }

        private void SynParentAccount(AccountRecord account)
        {
            List<AccountRecord> accounts = _account.Fetch(a => a.CompanyRecord.Id == _company.GetCurrentCompanyId()).ToList(); ;
            AccountRecord parent = accounts.Where(a => a.AccCode == account.ParentCode).FirstOrDefault();
            List<AccountRecord> childAccs = accounts.Where(a => a.ParentCode == account.ParentCode).ToList();

            if(parent != null)
            {
                parent.InitialBalance = childAccs.Sum(a => a.InitialBalance);
                parent.InitialQuantity = childAccs.Sum(a => a.InitialQuantity);
                parent.YtdDebit = childAccs.Sum(a => a.YtdDebit);
                parent.YtdDebitQuantity = childAccs.Sum(a => a.YtdDebitQuantity);
                parent.YtdCredit = childAccs.Sum(a => a.YtdCredit);
                parent.YtdCreditQuantity = childAccs.Sum(a => a.YtdCreditQuantity);
                parent.YtdBeginBalance = childAccs.Sum(a=>a.YtdBeginBalance);
                parent.YtdBeginBalanceQuantity = childAccs.Sum(a=>a.YtdBeginBalanceQuantity);

                _account.Update(parent);
            }
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
            List<VMAccount> vmAccounts = _mapper.Map<List<AccountRecord>, List<VMAccount>>(accounts);

            foreach (VMAccount a in vmAccounts)
            {
                if (a.IsAuxiliary == "on" || vmAccounts.Any(acc => acc.ParentCode == a.AccCode))
                {
                    a.HasChildren = true;
                }
                else
                {
                    a.HasChildren = false;
                }
            }

            return vmAccounts;
        }

        public List<int> QuantityCategory()
        {
            List<AccountCategoryRecord> cates = _accCate.GetMainAccountCategory();
            List<int> qCate = new List<int>();
            foreach(AccountCategoryRecord c in cates)
            {
                if (this.GetAccounts(c.Id).Any(a => a.IsQuantity == 1)) qCate.Add(c.Id);
            }
            return qCate;
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

            this.ClearAccountsCache();
        }

        public void SaveAccountInitData(VMAccount[] accounts)
        {
            foreach (VMAccount acc in accounts)
            {
                if (acc.AccId > 0)
                {
                    AccountRecord a = _account.Get(acc.AccId);
                    a.InitialQuantity = acc.InitialQuantity;
                    a.InitialBalance = acc.InitialBalance;
                    a.YtdDebitQuantity = acc.YtdDebitQuantity;
                    a.YtdDebit = acc.YtdDebit;
                    a.YtdCreditQuantity = acc.YtdCreditQuantity;
                    a.YtdCredit = acc.YtdCredit;
                    a.YtdBeginBalanceQuantity = a.InitialQuantity - a.YtdDebitQuantity + a.YtdCreditQuantity;
                    a.YtdBeginBalance = a.InitialBalance - a.YtdDebit + a.YtdCredit;


                    _account.Update(a);

                    this.SynParentAccount(a);
                }
            }

            this.ClearAccountsCache();
        }

        public void DeleteAccount(int accId)
        {
            AccountRecord accRec = _account.Get(accId);
            if(accRec !=  null)
            {
                _account.Delete(accRec);

                this.ClearAccountsCache();
            }
            else
            {
                throw new ArgumentNullException("找不到要删除的科目：" + accId);
            }
        }

        public void AddAuxItem(VMAccountAuxItem auxItem)
        {
            AccountRecord curAcct = this.GetAccount(auxItem.AccId);

            if(curAcct != null)
            {
                AccountRecord newAuxItem = new AccountRecord();

                newAuxItem.Direction = curAcct.Direction;
                newAuxItem.ParentCode = curAcct.AccCode;
                newAuxItem.IsAuxiliary = 0;
                newAuxItem.IsQuantity = 0;
                newAuxItem.AccountCategoryRecord = curAcct.AccountCategoryRecord;
                newAuxItem.CompanyRecord = _company.GetCurrentCompany().Record;
                newAuxItem.Creator = _authService.GetAuthenticatedUser().Id;
                newAuxItem.CreateTime = DateTime.Now;
                newAuxItem.Updater = _authService.GetAuthenticatedUser().Id;
                newAuxItem.UpdateTime = DateTime.Now;
                newAuxItem.AccState = 1;

                newAuxItem.AccCode = curAcct.AccCode;
                newAuxItem.AccName = curAcct.AccName;

                VMAuxiliary aux = null;
                if (auxItem.Custom.HasValue)
                {
                    aux = _auxiliary.GetAuxiliary(auxItem.Custom.Value);
                    if(aux != null)
                    {
                        newAuxItem.AccCode += "_" + aux.AuxCode;
                        newAuxItem.AccName += "_" + aux.AuxName;
                    }
                }

                if (auxItem.Suppliers.HasValue)
                {
                    aux = _auxiliary.GetAuxiliary(auxItem.Suppliers.Value);
                    if (aux != null)
                    {
                        newAuxItem.AccCode += "_" + aux.AuxCode;
                        newAuxItem.AccName += "_" + aux.AuxName;
                    }
                }

                if (auxItem.Employee.HasValue)
                {
                    aux = _auxiliary.GetAuxiliary(auxItem.Employee.Value);
                    if (aux != null)
                    {
                        newAuxItem.AccCode += "_" + aux.AuxCode;
                        newAuxItem.AccName += "_" + aux.AuxName;
                    }
                }

                if (auxItem.Project.HasValue)
                {
                    aux = _auxiliary.GetAuxiliary(auxItem.Project.Value);
                    if (aux != null)
                    {
                        newAuxItem.AccCode += "_" + aux.AuxCode;
                        newAuxItem.AccName += "_" + aux.AuxName;
                    }
                }

                if (auxItem.Sector.HasValue)
                {
                    aux = _auxiliary.GetAuxiliary(auxItem.Sector.Value);
                    if (aux != null)
                    {
                        newAuxItem.AccCode += "_" + aux.AuxCode;
                        newAuxItem.AccName += "_" + aux.AuxName;
                    }
                }

                if (auxItem.Inventory.HasValue)
                {
                    aux = _auxiliary.GetAuxiliary(auxItem.Inventory.Value);
                    if (aux != null)
                    {
                        newAuxItem.AccCode += "_" + aux.AuxCode;
                        newAuxItem.AccName += "_" + aux.AuxName;
                    }
                }

                _account.Create(newAuxItem);
            }

            this.ClearAccountsCache();
        }

        public void DeleteAuxItem(int accId)
        {
            AccountRecord acct = this.GetAccount(accId);
            if (acct != null)
            {
                _account.Delete(acct);

                this.ClearAccountsCache();
            }
        }

        #region 校验
        public string VAddAuxItem(VMAccountAuxItem auxItem)
        {
            if (auxItem.AccId <= 0)
            {
                return "数据无效";
            }

            if(!auxItem.Custom.HasValue && 
               !auxItem.Employee.HasValue && 
               !auxItem.Inventory.HasValue &&
               !auxItem.Project.HasValue && 
               !auxItem.Sector.HasValue &&
               !auxItem.Suppliers.HasValue)
            {
                return "未指定辅助核算内容";
            }

            return null;
        }

        public string VDeleteAuxItem(VMAccountAuxItem auxItem)
        {
            if(auxItem.AccId<=0)
            {
                return "数据无效";
            }

            var acct = this.GetAccount(auxItem.AccId);
            if(acct == null)
            {
                return "找不到要删除的记录";
            }

            return null;
        }
        #endregion

    }
}