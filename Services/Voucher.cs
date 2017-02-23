using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Caching.Services;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Security;
using AutoMapper;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Services
{
    public class Voucher
    {
        private readonly IMapper _mapper;
        private readonly IRepository<VoucherRecord> _voucher;
        private readonly IRepository<VoucherDetailRecord> _voucherDetail;
        private readonly ICompany _company;
        private readonly ICacheService _cache;
        private readonly ICertificateWord _certWord;
        private readonly IAuthenticationService _authService;
        private readonly IAccount _account;

        public Voucher(IMapper mapper,
                       IRepository<VoucherRecord> voucher,
                       IRepository<VoucherDetailRecord> voucherDetail,
                       ICompany company,
                       ICacheService cache,
                       ICertificateWord certWord,
                       IAuthenticationService authService,
                       IAccount account)
        {
            _mapper = mapper;
            _voucher = voucher;
            _voucherDetail = voucherDetail;
            _company = company;
            _cache = cache;
            _certWord = certWord;
            _authService = authService;
            _account = account;
        }

        private List<VoucherRecord> GetVouchersOfCompany()
        {
            int comId = _company.GetCurrentCompanyId();
            List<VoucherRecord> vouchers = _cache.Get<List<VoucherRecord>>(Common.GetVoucherCacheName(comId));

            if (vouchers == null || vouchers.Count == 0)
            {
                vouchers = _voucher.Fetch(v => v.CompanyRecord.Id == comId && v.PaymentTerms == DateTime.Now.Year).ToList();

                _cache.Put<List<VoucherRecord>>(Common.GetVoucherCacheName(comId), vouchers);
            }

            return vouchers;
        }

        private void ClearVoucherCache()
        {
            int comId = _company.GetCurrentCompanyId();
            _cache.Remove(Common.GetVoucherCacheName(comId));
        }

        public VoucherRecord GetVoucher(int voucherId)
        {
            return this.GetVouchersOfCompany().Where(v => v.Id == voucherId).FirstOrDefault();
        }

        public void SaveVoucher(VMVoucher vmVoucher)
        {
            VoucherRecord voucher = this.GetVoucher(vmVoucher.VId);
            if (voucher == null)
            {
                voucher = new VoucherRecord();
                voucher.CertWordSN = vmVoucher.CertWordSN;

                CertificateWordPart cw = _certWord.GetCertWordPartForCom(vmVoucher.CertWord);
                voucher.CertificateWord = cw.Record;
                voucher.Date = vmVoucher.Date;
                voucher.InvoiceCount = vmVoucher.InvoiceCount;
                voucher.State = 1;
                voucher.Creator = _authService.GetAuthenticatedUser().Id;
                voucher.CreateTime = DateTime.Now;
                
                foreach(VMVoucherDetail vd in vmVoucher.VoucherDetails)
                {
                    VoucherDetailRecord vdr = new VoucherDetailRecord();
                    vdr.Abstract = vdr.Abstract;

                    AccountRecord acct = _account.GetAccount(vd.AccountId);
                    vdr.AccountRecord = acct;
                    vdr.AccountCode = acct.AccCode;
                    vdr.AccountName = acct.AccName;
                    vdr.Quantity = vd.Quantity;
                    vdr.Price = vd.Price;
                    vdr.Debit = vd.Debit;
                    vdr.Credit = vd.Credit;

                    
                }
            }
        }
    }
}