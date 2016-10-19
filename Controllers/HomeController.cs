using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard;
using Orchard.Localization;

namespace XinTuo.Accounts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrchardServices _orchard;

        public HomeController(IOrchardServices orchard)
        {
            _orchard = orchard;
        }

        [Themed]
        public ActionResult Index()
        {
            return new ShapeResult(this,_orchard.New.Index);
        }
    }
}