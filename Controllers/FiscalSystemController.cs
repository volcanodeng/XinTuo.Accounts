using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc;
using Orchard;
using XinTuo.Accounts.Services;
using Orchard.Themes;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Controllers
{
    public class FiscalSystemController : Controller
    {
        private readonly IOrchardServices _orchard;
        private readonly ICompany _company;

        public FiscalSystemController(IOrchardServices orchard,ICompany company)
        {
            _orchard = orchard;
            _company = company;
        }

        [Themed]
        public ActionResult FiscalSystem()
        {
            var curCom = _company.GetCurrentCompany();
            if(curCom==null)
            {
                return new HttpUnauthorizedResult();
            }
            return new ShapeResult(this, _orchard.New.FiscalSystem(Company:curCom));
        }

        [Themed,HttpPost]
        public ActionResult SaveFiscal(VMFiscalSystem fiscal)
        {
            var curCom = _company.GetCurrentCompany();
            if (curCom == null)
            {
                return new HttpUnauthorizedResult();
            }

            _company.UpdateFiscalSystem(fiscal);

            return new ShapeResult(this, _orchard.New.Home_Index());
        }
    }
}