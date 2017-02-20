using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Mvc;
using Orchard;
using Orchard.Localization;
using Orchard.Themes;

namespace XinTuo.Accounts.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IOrchardServices _orchard;

        public VoucherController(IOrchardServices orchard)
        {
            _orchard = orchard;
        }

        public ActionResult Voucher()
        {
            return new ShapeResult(this,_orchard.New.Voucher());
        }
    }
}