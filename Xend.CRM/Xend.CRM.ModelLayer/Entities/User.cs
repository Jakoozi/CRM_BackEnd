﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.Entities
{
    public class User:BASE_ENTITY
    {
        public Guid? Company_Id { get; set; }
		public string Company_Name { get; set; }
		public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string XendCode { get; set; }
        public User_Role User_Role { get; set; }
		public string User_Password { get; set; }

        //This creates a relationship between the foriegn key tables and the AuditRail Table in my Database. 
        [ForeignKey("Company_Id")]
        public virtual Company Company { get; set; }
	}
}
