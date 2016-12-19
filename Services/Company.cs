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
using Orchard.Roles.Services;
using Orchard.Roles.Models;
using Orchard.Users.Events;
using Orchard;
using Orchard.Caching.Services;
using NHibernate;


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
        private readonly IRepository<UserRolesPartRecord> _userRole;
        private readonly IRoleService _role;
        private readonly IUserEventHandler _userEventHandler;
        private readonly IWorkContextAccessor _context;
        private readonly ITransactionManager _trans;
        private readonly ICacheService _cache;

        public Company(IAuthenticationService authService,
            IRepository<CompanyUserRecord> companyUser,
            IRepository<RegionRecord> region,
            IRepository<UserRolesPartRecord> userRolesRepository,
            IRoleService roleService,
            IContentManager contentManager,
            IWorkContextAccessor context,
            IMapper mapper,
            IMembershipService membership,
            IUserEventHandler userEventHandler,
            ITransactionManager trans,
            ICacheService cache)
        {
            _authService = authService;
            _companyUser = companyUser;
            _region = region;
            _contentManager = contentManager;
            _mapper = mapper;
            _membership = membership;
            _userRole = userRolesRepository;
            _role = roleService;
            _userEventHandler = userEventHandler;
            _context = context;
            _trans = trans;
            _cache = cache;
        }

        public CompanyPart GetCurrentCompany()
        {
            return _contentManager.Get<CompanyPart>(this.GetCurrentCompanyId());
        }

        public int GetCurrentCompanyId()
        {
            var com = _context.GetContext().HttpContext.Session["MyCompanyId"];
            if (com == null || Convert.ToInt32(com) <= 0)
            {
                IUser CurUser = _authService.GetAuthenticatedUser();
                if (CurUser == null) throw new UnauthorizedAccessException();

                CompanyUserRecord cuRecord = _companyUser.Fetch(cu => cu.UserPartRecord.Id == CurUser.Id).FirstOrDefault();
                if (cuRecord == null || cuRecord.CompanyRecord == null) throw new ObjectNotFoundException(CurUser.Id, typeof(CompanyUserRecord));


                _context.GetContext().HttpContext.Session["MyCompanyId"] = cuRecord.CompanyRecord.Id;
                com = cuRecord.CompanyRecord.Id;
            }

            return Convert.ToInt32(com);
        }

        public CompanyPart CreateCompany(VMCompany company)
        {

            var newCom = _contentManager.New("Company");
            CompanyPart newCompany = newCom.As<CompanyPart>();
            UserPart newUser = newCom.As<UserPart>();
            UserRolesPart userRole = newCom.As<UserRolesPart>();

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

            var accountant = _role.GetRoleByName("Accountant");
            if (accountant != null) _userRole.Create(new UserRolesPartRecord() { UserId = newUser.Id, Role = accountant });

            #region 新用户登录
            _userEventHandler.LoggingIn(company.ContractName, company.ContractName);
            var user = _membership.ValidateUser(company.ContractName, company.ContractName);
            if (user != null)
            {
                _authService.SignIn(user,false);
                _userEventHandler.LoggedIn(user);
            }
            #endregion

            return newCompany;
        }

        public void UpdateFiscalSystem(VMFiscalSystem fiscal)
        {
            CompanyPart com = GetCurrentCompany();

            //一个公司只能设置一次会计制度
            if(com.StartYear.HasValue && !string.IsNullOrEmpty(com.FiscalSystem))
            {
                return;
            }

            com.StartYear = fiscal.Year;
            com.StartPeriod = fiscal.Period;
            com.FiscalSystem = fiscal.Fiscal;

            _contentManager.Restore(com.ContentItem, VersionOptions.Latest);

            //初始化公司的会计科目
            ISQLQuery sqlQuery = _trans.GetSession().CreateSQLQuery("exec P_Account_Init @companyId=:comId,@creatorId=:userid");
            sqlQuery.SetInt32("comId", com.Id);
            sqlQuery.SetInt32("userid",_authService.GetAuthenticatedUser().Id);
            var newAccounts = sqlQuery.List<AccountRecord>();

            if(newAccounts.Count>0)
            {
                _cache.Put<IList<AccountRecord>>(Common.GetAccountsCacheName(com.Id), newAccounts);
            }

        }
    }
}