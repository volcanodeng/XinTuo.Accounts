using System.Web.Mvc;
using Orchard.Mvc;
using Orchard;

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