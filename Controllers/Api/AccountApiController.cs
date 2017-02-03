using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;
using Orchard;
using Orchard.Localization;
using System;


namespace XinTuo.Accounts.Controllers.Api
{
    public class AccountApiController : ApiController
    {
        private readonly IAccount _account;
        private readonly IAccountCategory _accCategory;
        private readonly IOrchardServices _orchard;
        private readonly ICertificateWord _certWord;

        public AccountApiController(IAccount account,
            IAccountCategory accCategory,
            IOrchardServices orchard,
            ICertificateWord certWord)
        {
            _account = account;
            _accCategory = accCategory;
            _orchard = orchard;
            _certWord = certWord;
        }

        [HttpGet]
        [ActionName("GetSubAccCate")]
        public IHttpActionResult GetAccountCategory(int cid)
        {
            return Ok(_accCategory.GetSubCategory(cid));
        }

        [HttpGet]
        [ActionName("GetAccCate")]
        public IHttpActionResult GetAccountCategory()
        {
            return Ok(_accCategory.GetMainAccountCategory());
        }

        [HttpGet]
        public IHttpActionResult GetAccounts(int cateid)
        {
            return Ok(_account.GetVMAccounts(cateid));
        }

        [HttpPost]
        [ActionName("Save")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAccount(VMAccount account)
        {
            string err;
            if(!ModelValidHelper.ModelValid(ModelState,out err))
            {
                return BadRequest(err);
            }

            if(!_orchard.Authorizer.Authorize(Permissions.ModifyAccount))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            _account.SaveAccount(account);

            return Ok(1);
        }

        [HttpGet]
        public IHttpActionResult GetCertWords()
        {
            return Ok(_certWord.GetCertificateWordForCom());
        }

        [HttpPost,ActionName("Delete")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DeleteAccount([FromUri]int accId)
        {
            if (!_orchard.Authorizer.Authorize(Permissions.ModifyAccount))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            _account.DeleteAccount(accId);
            
            return Ok(1);
        }
    }
}