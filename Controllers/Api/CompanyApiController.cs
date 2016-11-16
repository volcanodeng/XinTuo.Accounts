using System.Web.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.ViewModels;

namespace XinTuo.Accounts.Controllers.API
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
        public IHttpActionResult Save([FromBody]VMCompany com)
        {
            if(!ModelState.IsValid)
            {
                string err = "";
                foreach(var s in ModelState)
                {
                    foreach(var e in s.Value.Errors)
                    {
                        err += e.ErrorMessage;
                    }
                }
                return BadRequest(err);
            }

            var comPart = _company.CreateCompany(com);

            return Ok(comPart.Id);
        }
    }
}