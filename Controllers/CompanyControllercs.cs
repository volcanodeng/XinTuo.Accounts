using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard;
using XinTuo.Accounts.Services;

namespace XinTuo.Accounts.Controllers
{
    public class CompanyControllercs : Controller
    {
        private readonly IOrchardServices _orchard;
        private readonly ICompany _company;

        public CompanyControllercs(IOrchardServices orchard,ICompany company)
        {
            _orchard = orchard;
            _company = company;
        }

        public ActionResult Register()
        {
            return new ShapeResult(this,_orchard.New.Company());
        }
    }
}