using Orchard;
using XinTuo.Accounts.Models;
using System.Collections.Generic;

namespace XinTuo.Accounts.Services
{
    public interface IRegion : IDependency
    {
        List<RegionPartRecord> GetRegions(int? RegionId);
    }
}