using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using XinTuo.Accounts.ViewModels;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.AutoMapperProfiles
{
    public class VoucherProfile : Profile
    {
        public VoucherProfile()
        {
            CreateMap<VMVoucher, VoucherRecord>();

            CreateMap<VoucherRecord, VMVoucher>();

            CreateMap<VMVoucherDetail, VoucherDetailRecord>();

            CreateMap<VoucherDetailRecord, VMVoucherDetail>();
        }
    }
}