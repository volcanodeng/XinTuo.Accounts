using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Services
{
    public interface IAccount : IDependency
    {
        AccountRecord GetAccount(int id);

        List<AccountRecord> GetAccounts(int cateId);

        List<VMAccount> GetVMAccounts(int cateId);

        void SaveAccount(VMAccount account);

        void DeleteAccount(int accId);
    }
}