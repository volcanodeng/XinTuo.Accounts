using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard;
using Orchard.Localization;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.Models;
using Newtonsoft.Json;

namespace XinTuo.Accounts.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IOrchardServices _orchard;
        private readonly ICompany _company;

        public CompanyController(IOrchardServices orchard,ICompany company)
        {
            _orchard = orchard;
            _company = company;
        }

        public ActionResult Register()
        {
            CompanyPart com = _company.GetCurrentCompany();
            string comJson = null;
            if(com != null)
            {
                comJson = JsonConvert.SerializeObject(com.Record);
            }
            
            return new ShapeResult(this,_orchard.New.Company(Company: comJson));
        }

        public Localizer T { get; set; }
    }
}