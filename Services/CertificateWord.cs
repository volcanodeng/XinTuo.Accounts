using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using AutoMapper;
using Orchard.Security;

namespace XinTuo.Accounts.Services
{
    public class CertificateWord : ICertificateWord
    {
        private readonly IContentManager _contentManager;
        private readonly ICompany _company;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authService;

        public CertificateWord(IContentManager contentManager, 
                               ICompany company,
                               IMapper mapper,
                               IAuthenticationService authService)
        {
            _contentManager = contentManager;
            _company = company;
            _mapper = mapper;
            _authService = authService;
        }

        private IEnumerable<CertificateWordPart> GetCertificateWord(int companyId)
        {
            return _contentManager.Query<CertificateWordPart, CertificateWordRecord>().Where(c=>c.CompanyRecord.Id == companyId).List();
        }

        private void ClearDefaultCertWord(int comId)
        {
            var cws = GetCertificateWord(comId);
            var defCertWords = cws.Where(cw => cw.IsDefault == 1).ToList();

            foreach(CertificateWordPart cwp in defCertWords)
            {
                cwp.IsDefault = 0;
                _contentManager.Restore(cwp.ContentItem, VersionOptions.Latest);
            }

        }

        public List<VMCertWord> GetCertificateWordForCom()
        {
            int comId = _company.GetCurrentCompanyId();
            var cwList = GetCertificateWord(comId).ToList();

            return _mapper.Map<List<CertificateWordPart>, List<VMCertWord>>(cwList);
        }

        public VMCertWord GetCertificateWordForCom(int cwId)
        {
            List<VMCertWord> cwList = GetCertificateWordForCom();

            return cwList.Where(c => c.Id == cwId).FirstOrDefault();
        }

        public VMCertWord GetDefaultCertificateWordForCom()
        {
            List<VMCertWord> cwList = GetCertificateWordForCom();

            return cwList.Where(c => c.IsDefault == "on").FirstOrDefault();
        }

        public CertificateWordPart SaveCertWord(VMCertWord cw)
        {
            CertificateWordPart newCertWord = null;

            if(cw.IsDefault=="on")
            {
                this.ClearDefaultCertWord(_company.GetCurrentCompanyId());
            }

            if (cw.Id <= 0)
            {
                newCertWord = _contentManager.New<CertificateWordPart>("CertificateWord");

                newCertWord = _mapper.Map<VMCertWord, CertificateWordPart>(cw, newCertWord);

                CompanyPart com = _company.GetCurrentCompany();
                if (com != null) newCertWord.CompanyRecord = com.Record;

                IUser user = _authService.GetAuthenticatedUser();
                if (user != null) newCertWord.Creator = user.Id;

                newCertWord.CreateTime = DateTime.Now;

                _contentManager.Create(newCertWord);
            }
            else
            {
                newCertWord = _contentManager.Get<CertificateWordPart>(cw.Id);
                if(newCertWord != null)
                {
                    newCertWord = _mapper.Map<VMCertWord, CertificateWordPart>(cw,newCertWord);
                    _contentManager.Restore(newCertWord.ContentItem, VersionOptions.Latest);
                }
            }

            return newCertWord;
        }

        public void DeleteCertWord(int cwId)
        {
            var delCw = _contentManager.Get<CertificateWordPart>(cwId);
            if (delCw != null) _contentManager.Remove(delCw.ContentItem);
        }

        public void SetDefault(int cwId)
        {
            this.ClearDefaultCertWord(_company.GetCurrentCompanyId());
            var defCw = _contentManager.Get<CertificateWordPart>(cwId);
            defCw.IsDefault = 1;
            _contentManager.Restore(defCw.ContentItem,VersionOptions.Latest);
        }
    }
}