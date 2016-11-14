using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using XinTuo.Accounts.Services;
using XinTuo.Accounts.Models;
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
            if (com == null) return BadRequest("不能保存空内容");
            
            return Ok(_company.CreateCompany(com));
        }
    }
}