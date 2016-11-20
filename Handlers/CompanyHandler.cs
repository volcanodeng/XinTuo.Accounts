using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Handlers
{
    public class CompanyHandler : ContentHandler
    {
        public CompanyHandler(IRepository<CompanyRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<CompanyPart>("Company"));
        }
    }
}