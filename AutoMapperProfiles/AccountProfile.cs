using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using XinTuo.Accounts.ViewModels;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.AutoMapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<VMAccount, AccountRecord>().ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.AccId));
            CreateMap<AccountRecord, VMAccount>().ForMember(dest=>dest.AccId,opt=>opt.MapFrom(src=>src.Id));

            CreateMap<VMAuxiliary, AuxiliaryPart>();
            CreateMap<AuxiliaryPart, VMAuxiliary>().ForMember(dest=>dest.AuxId,opt=>opt.MapFrom(src=>src.Id));
        }

    }
}