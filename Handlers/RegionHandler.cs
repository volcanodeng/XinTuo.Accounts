using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Users.Models;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Handlers
{
    public class RegionHandler : ContentHandler
    {
        public RegionHandler(IRepository<CompanyRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}