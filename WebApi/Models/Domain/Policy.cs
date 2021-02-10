using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Domain
{
    public class Policy
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }
        public string PolicyNo { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Fullname { get; set; }
        public Address Address { get; set; }
        public Address StorageAddress { get; set; }
        public Reference Reference { get; }
        public ICollection<Address> Addresses { get; set; }
    }
}