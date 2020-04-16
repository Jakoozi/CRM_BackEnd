using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ModelLayer.ResponseModel
{
    public class ServiceResponseModel
    {
        public string successMessage = "Successful";
        public string errorMessage = "Error Occured";
        public int nullEntity = 1;
        public int existingEntity = 2;
        public int EntitySuccessfullyCreated = 3;
    }
}
