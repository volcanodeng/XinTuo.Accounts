using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using Orchard;

namespace XinTuo.Accounts.Services
{
    public interface ICompany : IDependency
    {
        CompanyPart GetCurrentCompany();

        CompanyPart CreateCompany(VMCompany company);

        void UpdateFiscalSystem(VMFiscalSystem fiscal);
    }
}