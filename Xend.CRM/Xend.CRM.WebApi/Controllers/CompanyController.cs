using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.WebApi.Controllers
{
    [Route("api/Company")]
    [ApiController]
    //meant to inherit from BaseController
    public class CompanyController : BaseAPIController
    {
        private readonly XendDbContext _context;

        [HttpPost]
        public IActionResult Create([FromBody] Company company)
        {
            if (company == null)
            {
                return BadRequest(1);
            }
            try
            {
               
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}