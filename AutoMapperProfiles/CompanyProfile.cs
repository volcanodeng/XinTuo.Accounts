using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using System.Data;

namespace XinTuo.Accounts.AutoMapperProfiles
{

    public class CompanyProfile : Profile
    {
        
        public CompanyProfile()
        {
            CreateMap<VMCompany, CompanyPart>();
            
        }
    }
}