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
            CreateMap<VMAccount, AccountPart>(MemberList.None);
            CreateMap<VMAuxiliary, AuxiliaryRecord>();
        }

    }
}