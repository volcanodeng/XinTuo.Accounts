using System.Collections.Generic;
using Orchard;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Services
{
    public interface IAuxiliary : IDependency
    {
        List<AuxiliaryTypeRecord> GetBaseAuxType();
    }
}