using System.Collections.Generic;
using Orchard;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Services
{
    public interface IAuxiliary : IDependency
    {
        List<AuxiliaryTypeRecord> GetBaseAuxType();

        AuxiliaryTypeRecord SaveAuxType(AuxiliaryTypeRecord customType);

        AuxiliaryPart SaveAuxiliary(VMAuxiliary aux);

    }
}