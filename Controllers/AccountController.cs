using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Themes;
using Orchard.Mvc;
using Orchard;
using Orchard.Localization;
using System.Web.Mvc;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.Models;

namespace XinTuo.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOrchardServices _orchard;
        private readonly IAccountCategory _accCategory;
        

        public AccountController(IOrchardServices orchard,IAccountCategory accCategory)
        {
            _orchard = orchard;
            _accCategory = accCategory;
        }


        public ActionResult Account()
        {
            List<AccountCategoryRecord> cates = _accCategory.GetMainAccountCategory();

            return new ShapeResult(this,_orchard.New.Account(Category : cates));
        }

        public Localizer T { get; set; }
    }
}