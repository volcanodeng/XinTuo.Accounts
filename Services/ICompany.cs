using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using Orchard;

namespace XinTuo.Accounts.Services
{
    public interface ICompany : IDependency
    {
        CompanyPart GetCurrentCompany();

        CompanyPart CreateCompany(VMCompany company);

        int GetCurrentCompanyId();

        void UpdateFiscalSystem(VMFiscalSystem fiscal);
    }
}