﻿using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ResponseModel.ServiceModels
{
	public class TeamServiceResponseModel
	{
		public Team team = new Team();
		public string Message { get; set; }
		public string code { get; set; }
	}

}
