using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using Orchard;

namespace XinTuo.Accounts.Services
{
    public interface ICompany : IDependency
    {
        CompanyPart GetCurrentCompany();
    }
}