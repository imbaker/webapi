using System.Collections.Generic;

namespace WebApi.Models.Domain
{
    public interface IPolicyTransaction
    {
        string PolicyId { get; set; }

        List<Address> Addresses { get; set; }

        void Load();
    }
}
