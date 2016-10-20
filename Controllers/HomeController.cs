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

namespace XinTuo.Accounts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrchardServices _orchard;
        private readonly IRegion _region;

        public HomeController(IOrchardServices orchard,IRegion region)
        {
            _orchard = orchard;
            _region = region;
        }

        [Themed]
        public ActionResult Index()
        {
            List<RegionRecord> regions = _region.GetRegions(null);
            return new ShapeResult(this, _orchard.New.Index(regions:regions));
        }
    }
}