using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Dto
{
    public class AdjustmentDto
    {
        public int Id { get; set; }
        public int OriginalPolicyId { get; set; }
        public int AdjustmentCode { get; set; }
        public DateTime LastUpdated { get; set; }
        public Reference Reference { get; }
    }
}