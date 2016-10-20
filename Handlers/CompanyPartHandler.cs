using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Users.Models;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Handlers
{
    public class CompanyPartHandler : ContentHandler
    {
        public CompanyPartHandler(IRepository<CompanyRecord> repository)
        {
            //Filters.Add(StorageFilter.For(repository));
            //Filters.Add(new ActivatingFilter<UserPart>("Company"));
        }
    }
}