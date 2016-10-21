using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using Orchard.Data;
using Orchard.Security;

namespace XinTuo.Accounts.Services
{
    public class Company
    {
        private readonly IRepository<CompanyRecord> _companyRecord;
        private readonly IAuthenticationService _authService;

        public Company(IRepository<CompanyRecord> companyRecord, IAuthenticationService authService)
        {
            _companyRecord = companyRecord;
            _authService = authService;
        }

        public CompanyRecord GetCurrentCompany()
        {
            return null;
        }
    }
}