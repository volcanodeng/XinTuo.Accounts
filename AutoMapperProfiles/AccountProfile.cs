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
            CreateMap<VMAccount, AccountRecord>()
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>src.AccId))
                .ForMember(dest=>dest.IsAuxiliary,opt=>opt.ResolveUsing<CheckboxResolver,string>(src=>src.IsAuxiliary))
                .ForMember(dest => dest.IsQuantity, opt => opt.ResolveUsing<CheckboxResolver, string>(src => src.IsQuantity));
            CreateMap<AccountRecord, VMAccount>()
                .ForMember(dest=>dest.AccId,opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.IsQuantity,opt=>opt.ResolveUsing<ToCheckboxResolver,int>(src=>src.IsQuantity))
                .ForMember(dest => dest.IsAuxiliary, opt => opt.ResolveUsing<ToCheckboxResolver, int>(src => src.IsAuxiliary))
                .ForMember(dest=>dest.cateName,opt=>opt.MapFrom(src=>src.AccountCategoryRecord.CateName));

            CreateMap<VMAuxiliary, AuxiliaryPart>();
            CreateMap<AuxiliaryPart, VMAuxiliary>().ForMember(dest=>dest.AuxId,opt=>opt.MapFrom(src=>src.Id));


            CreateMap<VMCertWord, CertificateWordPart>()
                .ForMember(dest => dest.IsDefault, opt => opt.ResolveUsing<CheckboxResolver, string>(src => src.IsDefault));
            CreateMap<CertificateWordPart, VMCertWord>()
                .ForMember(dest => dest.IsDefault, opt => opt.ResolveUsing<ToCheckboxResolver, int>(src => src.IsDefault));
        }

    }


}