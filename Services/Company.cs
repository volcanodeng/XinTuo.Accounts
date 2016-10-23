using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using Orchard.Data;
using Orchard.Security;
using Orchard.ContentManagement;

namespace XinTuo.Accounts.Services
{
    public class Company
    {
        private readonly IAuthenticationService _authService;
        private readonly IRepository<CompanyUserRecord> _companyUser;
        private readonly IContentManager _contentManager;

        public Company(IAuthenticationService authService,
            IRepository<CompanyUserRecord> companyUser,
            IContentManager contentManager)
        {
            _authService = authService;
            _companyUser = companyUser;
            _contentManager = contentManager;
        }

        public CompanyPart GetCurrentCompany()
        {
            IUser CurUser = _authService.GetAuthenticatedUser();
            if (CurUser == null) return null;

            CompanyUserRecord cuRecord = _companyUser.Fetch(cu => cu.UserPartRecord.Id == CurUser.Id).FirstOrDefault();
            if (cuRecord == null) return null;

            return _contentManager.Get<CompanyPart>(cuRecord.CompanyRecord.Id);
        }
    }
}