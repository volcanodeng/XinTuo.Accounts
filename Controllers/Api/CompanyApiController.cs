using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Controllers.Api
{
    public class CompanyApiController : ApiController
    {
        private readonly ICompany _company;
        private readonly IRegion _region;

        

        public CompanyApiController(ICompany company,IRegion region)
        {
            _company = company;
            _region = region;
        }

        [HttpGet]
        [ActionName("GetRegion")]
        public IHttpActionResult GetRegion(int id)
        {
            return Ok(_region.GetRegions(id));
        }

        [HttpGet]
        [ActionName("GetProvinces")]
        public IHttpActionResult GetRegion()
        {
            return Ok(_region.GetRegions(null));
        }

      

        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Save([FromBody]VMCompany com)
        {
            string err;
            if(!ModelValidHelper.ModelValid(ModelState,out err))
            {
                return BadRequest(err);
            }

            var comPart = _company.CreateCompany(com);

            return Ok(comPart.Id);
        }
    }
}