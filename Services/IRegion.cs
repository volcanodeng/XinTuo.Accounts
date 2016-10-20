using Orchard;
using XinTuo.Accounts.Models;
using System.Collections.Generic;

namespace XinTuo.Accounts.Services
{
    public interface IRegion : IDependency
    {
        List<RegionRecord> GetRegions(int? RegionId);

        List<RegionRecord> test();
    }
}