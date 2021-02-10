using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Domain.Guardian
{
    public class Content
    {
        public string ApiUrl  { get; set; }
        public string Id { get; set; }
        public bool IsHosted { get; set; }
        public string WebTitle { get; set; }
        public string WebUrl { get; set; }
    }
}