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
    public class AuxiliaryController : Controller
    {
        private readonly IAuxiliary _auxiliary;
        private readonly IOrchardServices _orchard;

        public AuxiliaryController(IOrchardServices orchard, IAuxiliary auxiliary)
        {
            _auxiliary = auxiliary;
            _orchard = orchard;
        }

        public ActionResult AuxiliaryType()
        {
            List<AuxiliaryTypeRecord> auxType = _auxiliary.GetBaseAuxType();
            return new ShapeResult(this, _orchard.New.AuxiliaryType(AuxiliaryType: auxType));
        }

        public ActionResult AuxiliaryTypeEdit(int id)
        {
            List<AuxiliaryTypeRecord> baseAuxTypes = _auxiliary.GetBaseAuxType();
            var auxType = baseAuxTypes.Where(at => at.Id == id).FirstOrDefault();
            return new ShapeResult(this, _orchard.New.AuxiliaryTypeEdit(AuxType: auxType,BaseAuxTypes:baseAuxTypes));
        }
    }
}