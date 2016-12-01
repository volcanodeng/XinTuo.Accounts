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
        private readonly IRoleService _role;

        public HomeController(IOrchardServices orchard,IRoleService role)
        {
            _orchard = orchard;
            _role = role;
        }

        
        public ActionResult Index()
        {
            var roles = ((ContentItem)_orchard.WorkContext.CurrentUser.ContentItem).As<UserRolesPart>().Roles;
            var p = _role.GetPermissionsForRole(9);
            return new ShapeResult(this, _orchard.New.Index(obj:roles));
        }
    }
}