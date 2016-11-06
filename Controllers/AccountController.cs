using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard;
using Orchard.Localization;
using System.Web.Mvc;

namespace XinTuo.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOrchardServices _orchard;
        

        public AccountController(IOrchardServices orchard)
        {
            _orchard = orchard;
        }


        public ActionResult Account()
        {
            return new ShapeResult(this,_orchard.New.Account());
        }

        public Localizer T { get; set; }
    }
}