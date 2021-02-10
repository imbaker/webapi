using System.Collections.Generic;

namespace WebApi.Models.Dto
{
    public class PolicyTransaction : IPolicyTransaction
    {
        public string PolicyId { get; set; }

        public List<AddressDto> Addresses { get; set; }

        public PolicyTransaction()
        {
            Addresses = new List<AddressDto>();
        }
    }
}