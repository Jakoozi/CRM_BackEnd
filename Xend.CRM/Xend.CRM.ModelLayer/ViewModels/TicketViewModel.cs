﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class TicketViewModel : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
        public Guid Customer_Id { get; set; }
        public Guid Createdby_Userid { get; set; }
        public Guid? Resolvedby_Userid { get; set; }
        public Guid? Resolvedby_Teamid { get; set; }
        //the ? after the Guid is for nullable entity value
        public string Ticket_Subject { get; set; }
        public string Ticket_Details { get; set; }
        public Ticket_Status Ticket_Status { get; set; }

        [ForeignKey("Company_Id")]
        public virtual Company Company { get; set; }

        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("Createdby_Userid")]
        public virtual User CreatedUser { get; set; }

        [ForeignKey("Resolvedby_Userid")]
        public virtual User ResolvedUser { get; set; }

        [ForeignKey("Resolvedby_Teamid")]
        public virtual Team Team { get; set; }
    }
}