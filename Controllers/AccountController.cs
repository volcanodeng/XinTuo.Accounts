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
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOrchardServices _orchard;
        private readonly IAccountCategory _accCategory;
        private readonly IAuxiliary _auxiliary;
        private readonly IAccount _account;


        public AccountController(IOrchardServices orchard,
                                 IAccountCategory accCategory, 
                                 IAuxiliary auxiliary,
                                 IAccount account)
        {
            _orchard = orchard;
            _accCategory = accCategory;
            _auxiliary = auxiliary;
            _account = account;
        }


        public ActionResult Account()
        {
            List<AccountCategoryRecord> cates = _accCategory.GetMainAccountCategory();
            List<AuxiliaryTypeRecord> auxType = _auxiliary.GetBaseAuxType();

            return new ShapeResult(this,_orchard.New.Account(Category : cates, AuxiliaryType: auxType));
        }

        public ActionResult InitialBalance()
        {
            List<AccountCategoryRecord> cates = _accCategory.GetMainAccountCategory();
            var qCate = _account.QuantityCategory();

            return new ShapeResult(this, _orchard.New.InitialBalance(Category: cates, QuantityCategory: string.Join(",", qCate)));
        }

        public ActionResult CertWord()
        {
            return new ShapeResult(this, _orchard.New.CertWord());
        }

        public Localizer T { get; set; }
    }
}