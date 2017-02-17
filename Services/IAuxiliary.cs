using System.Collections.Generic;
using Orchard;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Services
{
    public interface IAuxiliary : IDependency
    {
        List<AuxiliaryTypeRecord> GetBaseAuxType();

        AuxiliaryTypeRecord SaveAuxType(string auxTypeName);

        AuxiliaryPart SaveAuxiliary(VMAuxiliary aux);

        void DeleteAuxiliary(int auxId);

        IEnumerable<AuxiliaryPart> GetAuxiliaries(int companyId, int auxTypeId);

        List<VMAuxiliary> GetAuxiliaryForCom(int auxTypeId);

        VMAuxiliary GetAuxiliary(int auxId);
    }
}