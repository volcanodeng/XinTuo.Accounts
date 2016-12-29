using XinTuo.Accounts.ViewModels;
using Orchard;

namespace XinTuo.Accounts.Services
{
    public interface IFiscalSystem : IDependency
    {
        void UpdateFiscalSystem(VMFiscalSystem fiscal);
    }
}