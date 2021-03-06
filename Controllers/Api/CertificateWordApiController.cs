﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;
using Orchard;
using Orchard.Logging;

namespace XinTuo.Accounts.Controllers.Api
{
    public class CertificateWordApiController : ApiController
    {
        private ICertificateWord _certWord;
        private readonly IOrchardServices _orchard;

        public CertificateWordApiController(ICertificateWord certWord, IOrchardServices orchard)
        {
            _certWord = certWord;
            _orchard = orchard;
        }

        [HttpGet,ActionName("GetComCertWord")]
        public IHttpActionResult GetCertificateWord()
        {
            if (_orchard.WorkContext.CurrentUser == null)
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            return Ok(_certWord.GetCertificateWordForCom());
        }

        [HttpGet, ActionName("GetCertWord")]
        public IHttpActionResult GetCertificateWord(int cwId)
        {
            if (_orchard.WorkContext.CurrentUser == null)
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            return Ok(_certWord.GetCertificateWordForCom(cwId));
        }

        [HttpPost,ActionName("SaveCertWord")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveCerttificateWord([FromBody] VMCertWord certWord)
        {
            string err;
            if (!ModelValidHelper.ModelValid(ModelState, out err))
            {
                return BadRequest(err);
            }

            if (!_orchard.Authorizer.Authorize(Permissions.ModifyAccount))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            var newCertWord = _certWord.SaveCertWord(certWord);
            if (newCertWord == null)
            {
                var msg = new ApiResponse("找不到要处理的数据", System.Net.HttpStatusCode.NoContent);
                msg.Logger.Warning("记录不存在。data:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(certWord.PrintTitle));
                throw new HttpResponseException(msg);
            }

            return Ok(newCertWord.Id);
        }

        [HttpGet,ActionName("DelCertWord")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteCertificateWord(int cwId)
        {
            if (!_orchard.Authorizer.Authorize(Permissions.ModifyAccount))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            if (cwId <= 0)
            {
                return BadRequest("待删除记录无效");
            }

            _certWord.DeleteCertWord(cwId);

            return Ok(1);
        }

        [HttpGet, ActionName("SetDefault")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SetDefault(int cwId)
        {
            if (!_orchard.Authorizer.Authorize(Permissions.ModifyAccount))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            if (cwId <= 0)
            {
                return BadRequest("记录无效");
            }

            _certWord.SetDefault(cwId);

            return Ok(1);
        }
    }
}