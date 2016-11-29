using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;
using Orchard;
using Orchard.Localization;


namespace XinTuo.Accounts.Controllers.Api
{
    public class AccountApiController : ApiController
    {
        private readonly IAccount _account;
        private readonly IAccountCategory _accCategory;
        private readonly IOrchardServices _orchard;

        public AccountApiController(IAccount account,IAccountCategory accCategory,IOrchardServices orchard)
        {
            _account = account;
            _accCategory = accCategory;
            _orchard = orchard;
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

            if(!_orchard.Authorizer.Authorize(Permissions.CreateAccount))
            {
                var msg = new ApiResponse("未授权访问", System.Net.HttpStatusCode.Unauthorized);
                throw new HttpResponseException(msg);
            }

            return Ok(_account.SaveAccount(account));
        }

        
    }
}