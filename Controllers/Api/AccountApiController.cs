using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;
using Orchard;
using Orchard.Localization;
using System;
using System.Collections.Generic;


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

        [HttpPost]
        [ActionName("SaveInitData")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult SaveAccounts(VMAccountsWrap accounts)
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

            _account.SaveAccountInitData(accounts.Accounts);

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

        [HttpPost, ActionName("AddAuxItem")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult AddAuxItem(VMAccountAuxItem auxItem)
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

            string validMsg = _account.VAddAuxItem(auxItem);
            if(!string.IsNullOrEmpty(validMsg))
            {
                throw new HttpResponseException(new ApiResponse(validMsg, System.Net.HttpStatusCode.Forbidden));
            }

            _account.AddAuxItem(auxItem);

            return Ok(1);
        }

        [HttpPost, ActionName("DelAuxItem")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult DelAuxItem(VMAccountAuxItem auxItem)
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

            string validMsg = _account.VDeleteAuxItem(auxItem);
            if (!string.IsNullOrEmpty(validMsg))
            {
                throw new HttpResponseException(new ApiResponse(validMsg, System.Net.HttpStatusCode.Forbidden));
            }

            _account.DeleteAccount(auxItem.AccId);

            return Ok(1);
        }
    }
}