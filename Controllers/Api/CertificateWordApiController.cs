using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;
using Orchard;

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
    }
}