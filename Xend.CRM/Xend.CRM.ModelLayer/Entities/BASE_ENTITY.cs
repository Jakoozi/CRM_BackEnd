using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.Entities
{
    public class BASE_ENTITY
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public double CreatedAtTimeStamp { get; set; }
        public double UpdatedAtTimeStamp { get; set; }
        public EntityStatus Status { get; set; }
    }
}
