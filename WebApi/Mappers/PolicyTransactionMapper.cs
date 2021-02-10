using WebApi.Models.Domain;

namespace WebApi.Mappers
{
    public class PolicyTransactionMapper : IPolicyTransactionMapper
    {
        public void Map(Models.Dto.IPolicyTransaction from, Models.Domain.IPolicyTransaction to)
        {
            to.PolicyId = from.PolicyId;

            foreach (var address in from.Addresses)
            {
                var domainAddress = new Address()
                {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    Line3 = address.Line3,
                    Town = address.Town
                };
                to.Addresses.Add(domainAddress);
            }
        }
    }
}