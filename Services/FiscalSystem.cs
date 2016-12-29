using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using Orchard.Security;
using NHibernate;

namespace XinTuo.Accounts.Services
{
    public class FiscalSystem : IFiscalSystem
    {
        private readonly IContentManager _contentManager;
        private readonly ITransactionManager _trans;
        private readonly ICompany _company;
        private readonly IAuthenticationService _authService;
        private readonly ICertificateWord _certWord;

        public FiscalSystem(IContentManager contentManager,
                            ICompany company,
                            ITransactionManager trans,
                            IAuthenticationService authService,
                            ICertificateWord certWord)
        {
            _contentManager = contentManager;
            _trans = trans;
            _company = company;
            _authService = authService;
            _certWord = certWord;
        }

        public void UpdateFiscalSystem(VMFiscalSystem fiscal)
        {
            CompanyPart com = _company.GetCurrentCompany();

            //一个公司只能设置一次会计制度
            if (com.StartYear.HasValue && !string.IsNullOrEmpty(com.FiscalSystem))
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
            sqlQuery.SetInt32("userid", _authService.GetAuthenticatedUser().Id);
            int res = sqlQuery.ExecuteUpdate();

            //初始化凭证字
            VMCertWord cw = new VMCertWord();
            cw.CertWord = "记";
            cw.PrintTitle = "记账凭证";
            cw.IsDefault = "on";
            cw.SortIndex = 1;
            _certWord.SaveCertWord(cw);

        }
    }
}