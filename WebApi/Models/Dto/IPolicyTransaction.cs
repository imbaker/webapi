using System.Collections.Generic;

namespace WebApi.Models.Dto
{
    public interface IPolicyTransaction
    {
        string PolicyId { get; set; }

        List<AddressDto> Addresses { get; set; }
    }
}
