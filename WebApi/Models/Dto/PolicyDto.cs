using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Dto
{
    public class PolicyDto
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }
        public string PolicyNo { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public AddressDto Address { get; set; }
        public AddressDto StorageAddress { get; set; }
        public Reference Reference { get; }
        public ICollection<AddressDto> Addresses { get; set; }
    }
}