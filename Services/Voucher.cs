using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Caching.Services;
using Orchard.ContentManagement;
using Orchard.Data;
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

        public Voucher(IMapper mapper,
                       IRepository<VoucherRecord> voucher,
                       IRepository<VoucherDetailRecord> voucherDetail,
                       ICompany company,
                       ICacheService cache)
        {
            _mapper = mapper;
            _voucher = voucher;
            _voucherDetail = voucherDetail;
            _company = company;
            _cache = cache;
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

        public void SaveVoucher(VMVoucher voucher)
        {

        }
    }
}