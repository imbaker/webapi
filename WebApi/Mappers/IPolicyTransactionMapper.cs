using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Domain;
using WebApi.Models.Dto;

namespace WebApi.Mappers
{
    public interface IPolicyTransactionMapper
    {
        void Map(Models.Dto.IPolicyTransaction from, Models.Domain.IPolicyTransaction to);
    }
}
