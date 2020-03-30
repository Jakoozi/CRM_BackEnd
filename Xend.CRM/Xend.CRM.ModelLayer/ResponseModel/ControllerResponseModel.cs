//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Xend.CRM.ModelLayer.ResponseModel
//{
//    public class ControllerResponseModel
//    {
//        public BadRequestObjectResult

//    }
//}


//public OkObjectResult Ok(object value, string message = "", string code = null)
//{
//    return base.Ok(new
//    {
//        Status = StaticFields.success,
//        status_code = 200,
//        Code = code,
//        Message = message,
//        Data = value
//    });
//}
//public BadRequestObjectResult BadRequest(object value, string message = "", int status_code = 500, string code = "")
//{
//    return base.BadRequest(new
//    {
//        Status = StaticFields.failure,
//        Status_code = status_code,
//        Code = code,
//        Message = message,
//        Data = value
//    });
//}