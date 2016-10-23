using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.Models;

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
            if(com == null)
            {
                com = new CompanyPart();
            }
            return new ShapeResult(this,_orchard.New.Company(Company:com));
        }
    }
}