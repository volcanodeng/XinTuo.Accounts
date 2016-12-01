using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;
using System.Text;
using Orchard.Security;
using Orchard;

namespace XinTuo.Accounts.Controllers.Api
{
    public class AuxiliaryApiController : ApiController
    {
        private readonly IAuxiliary _aux;
        private readonly IAuthenticationService _authService;
        private readonly IOrchardServices _orchard;

        public AuxiliaryApiController(IAuxiliary aux, IAuthenticationService authService, IOrchardServices orchard)
        {
            _aux = aux;
            _authService = authService;
            _orchard = orchard;
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

            if (!_orchard.Authorizer.Authorize(Permissions.CreateAuxiliary))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            if (!aux.AuxId.HasValue || aux.AuxId.Value == 0) aux.AuxState = 1;

            var auxiliary = _aux.SaveAuxiliary(aux);

            return Ok(auxiliary.Id);
        }

        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetAuxiliary(int auxTypeId)
        {
            if (_orchard.WorkContext.CurrentUser == null)
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            return Ok(_aux.GetAuxiliaryForCom(auxTypeId));
        }
    }
}