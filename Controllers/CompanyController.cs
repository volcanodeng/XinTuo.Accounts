using System.Web.Mvc;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard;
using Orchard.Localization;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.Models;
using XinTuo.Accounts.ViewModels;
using Newtonsoft.Json;
using AutoMapper;

namespace XinTuo.Accounts.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IOrchardServices _orchard;
        private readonly ICompany _company;
        private readonly IMapper _mapper;

        public CompanyController(IOrchardServices orchard,ICompany company,IMapper mapper)
        {
            _orchard = orchard;
            _company = company;
            _mapper = mapper;
        }


        public ActionResult Register()
        {
            CompanyPart com = _company.GetCurrentCompany();
            VMCompany company = null;
            if(com != null)
            {
                company = _mapper.Map<CompanyPart, VMCompany>(com);
                company = _mapper.Map<RegionRecord, VMCompany>(com.Region,company);
            }
            if(company == null)
            {
                company = new VMCompany();
            }
            
            return new ShapeResult(this,_orchard.New.Company(Company: company));
        }

        public Localizer T { get; set; }
    }
}