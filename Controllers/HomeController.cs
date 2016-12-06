using System.Web.Mvc;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard.Roles.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Roles.Services;
using System.Collections.Generic;

namespace XinTuo.Accounts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrchardServices _orchard;

        public HomeController(IOrchardServices orchard)
        {
            _orchard = orchard;
        }

        public ActionResult Index()
        {
            
            return new ShapeResult(this, _orchard.New.Index());
        }
    }
}