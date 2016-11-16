using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;


namespace XinTuo.Accounts.Controllers.Api
{
    public class AccountApiController : ApiController
    {
        private readonly IAccount _account;
        private readonly IAccountCategory _accCategory;

        public AccountApiController(IAccount account,IAccountCategory accCategory)
        {
            _account = account;
            _accCategory = accCategory;
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
    }
}