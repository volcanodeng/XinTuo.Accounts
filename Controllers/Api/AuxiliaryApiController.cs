﻿using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;
using Orchard.Security;
using Orchard;
using Orchard.Logging;

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

            if (!_orchard.Authorizer.Authorize(Permissions.ModifyAuxiliary))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            if (!aux.AuxId.HasValue || aux.AuxId.Value == 0) aux.AuxState = 1;

            var auxiliary = _aux.SaveAuxiliary(aux);

            if(auxiliary == null)
            {
                var msg = new ApiResponse("找不到要处理的数据", System.Net.HttpStatusCode.NoContent);
                msg.Logger.Warning("记录不存在。data:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(aux));
                throw new HttpResponseException(msg);
            }

            return Ok(auxiliary.Id);
        }

        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAuxType([FromBody]VMAuxiliaryType auxType)
        {
            string err;
            if (!ModelValidHelper.ModelValid(ModelState, out err))
            {
                return BadRequest(err);
            }

            if (!_orchard.Authorizer.Authorize(Permissions.ModifyAuxiliary))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            if(string.IsNullOrWhiteSpace(auxType.auxTypeName))
            {
                return BadRequest("核算类型名称不能为空");
            }

            var newAuxType = _aux.SaveAuxType(auxType.auxTypeName);
            return Ok(newAuxType.Id);
        }

        [HttpGet]
        [ActionName("DelAux")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAuxiliary(int auxId)
        {
           
            if (!_orchard.Authorizer.Authorize(Permissions.ModifyAuxiliary))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            if(auxId <= 0)
            {
                return BadRequest("待删除记录无效");
            }

            _aux.DeleteAuxiliary(auxId);

            return Ok(1);
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