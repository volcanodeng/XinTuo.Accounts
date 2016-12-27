using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data;
using Orchard.ContentManagement;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using AutoMapper;

namespace XinTuo.Accounts.Services
{
    public class CertificateWord : ICertificateWord
    {
        private readonly IContentManager _contentManager;
        private readonly ICompany _company;
        private readonly IMapper _mapper;

        public CertificateWord(IContentManager contentManager, 
                               ICompany company,
                               IMapper mapper)
        {
            _contentManager = contentManager;
            _company = company;
            _mapper = mapper;
        }

        private IEnumerable<CertificateWordPart> GetCertificateWord(int companyId)
        {
            return _contentManager.Query<CertificateWordPart, CertificateWordRecord>().Where(c=>c.CompanyRecord.Id == companyId).List();
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
    }
}