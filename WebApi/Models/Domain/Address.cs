using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Domain
{
    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}