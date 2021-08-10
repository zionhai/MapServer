using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapServer.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ISiteRepository SiteRepo;
        
        public SiteController(ISiteRepository siteRepo)
        {
            SiteRepo = siteRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(SiteRepo.GetSites());
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }

        [HttpGet]
        [Route("last_sample")]
        public async Task<IActionResult> LastSample()
        {

            try
            {
                return Ok(SiteRepo.GetLastSample());
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
