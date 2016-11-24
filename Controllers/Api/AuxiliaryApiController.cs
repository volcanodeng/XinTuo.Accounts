using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Controllers.Api
{
    public class AuxiliaryApiController : ApiController
    {
        private readonly IAuxiliary _aux;

        public AuxiliaryApiController(IAuxiliary aux)
        {
            _aux = aux;
        }

        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Save([FromBody]VMAuxiliary aux)
        {
            string err;
            if(!ModelValidHelper.ModelValid(ModelState,out err))
            {
                return BadRequest(err);
            }

            if (aux.Id == 0) aux.AuxState = 1;

            var auxiliary = _aux.SaveAuxiliary(aux);

            return Ok(auxiliary.Id);
        }
    }
}