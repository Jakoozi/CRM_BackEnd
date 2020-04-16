using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xend.CRM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseAPIController : Controller
    {
        [NonAction]
        public OkObjectResult Ok(object value, string message = "", string code = "")
        {
            return base.Ok(new
            {
                Status = StaticFields.success,
                status_code = 200,
                Code = code,
                Message = message,
                Data = value
            });
        }
        [NonAction]
        public BadRequestObjectResult BadRequest(object value, string message = "", string code = "")
        {
            return base.BadRequest(new
            {
                Status = StaticFields.failure,
                Status_code = 400,
                Code = code,
                Message = message,
                Data = value
            });
        }
    }
}