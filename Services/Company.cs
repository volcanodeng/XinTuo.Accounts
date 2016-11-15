using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using Orchard.Data;
using Orchard.Security;
using Orchard.ContentManagement;
using AutoMapper;
using Orchard.Users.Models;

namespace XinTuo.Accounts.Services
{
    public class Company : ICompany
    {
        private readonly IAuthenticationService _authService;
        private readonly IRepository<CompanyUserRecord> _companyUser;
        private readonly IRepository<RegionRecord> _region;
        private readonly IContentManager _contentManager;
        private readonly IMapper _mapper;
        private readonly IMembershipService _membership;

        public Company(IAuthenticationService authService,
            IRepository<CompanyUserRecord> companyUser,
            IRepository<RegionRecord> region,
            IContentManager contentManager,
            IMapper mapper,
            IMembershipService membership)
        {
            _authService = authService;
            _companyUser = companyUser;
            _region = region;
            _contentManager = contentManager;
            _mapper = mapper;
            _membership = membership;
        }

        public CompanyPart GetCurrentCompany()
        {
            IUser CurUser = _authService.GetAuthenticatedUser();
            if (CurUser == null) return null;

            CompanyUserRecord cuRecord = _companyUser.Fetch(cu => cu.UserPartRecord.Id == CurUser.Id).FirstOrDefault();
            if (cuRecord == null) return null;

            return _contentManager.Get<CompanyPart>(cuRecord.CompanyRecord.Id);
        }

        public CompanyPart CreateCompany(VMCompany company)
        {

            var newCom = _contentManager.New("Company");
            CompanyPart newCompany = newCom.As<CompanyPart>();
            UserPart newUser = newCom.As<UserPart>();

            newCompany = _mapper.Map<VMCompany, CompanyPart>(company,newCompany);
            newCompany.Region = _region.Fetch(r => r.RegionId == company.RegionId).FirstOrDefault();
            

            newUser.UserName = company.ContractName;
            newUser.NormalizedUserName = company.ContractName;
            newUser.Email = company.ContractEmail;
            newUser.HashAlgorithm = "SHA1";
            newUser.Record.PasswordFormat = System.Web.Security.MembershipPasswordFormat.Hashed;
            newUser.Record.RegistrationStatus = UserStatus.Approved;
            newUser.Record.EmailStatus = UserStatus.Approved;
            newUser.CreatedUtc = DateTime.Now;

            _membership.SetPassword(newUser, company.ContractName);

            _contentManager.Create(newCom);

            _companyUser.Create(new CompanyUserRecord() { CompanyRecord = newCompany.Record, UserPartRecord = newUser.Record });

            return newCompany;
        }
    }
}