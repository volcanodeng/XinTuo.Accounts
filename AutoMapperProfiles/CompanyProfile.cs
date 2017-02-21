using AutoMapper;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.AutoMapperProfiles
{

    public class CompanyProfile : Profile
    {
        
        public CompanyProfile()
        {
            CreateMap<VMCompany, CompanyPart>();
            CreateMap<CompanyPart, VMCompany>();
            CreateMap<RegionRecord, VMCompany>();
            CreateMap<VMCompany, RegionRecord>();
        }
    }
}