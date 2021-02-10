using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.Domain.Guardian;

namespace WebApi.Repositories
{
    public interface INewsRepository
    {
        Task<Response> Get(string id);

        Task<Response> Search(string searchParameter);

        Task<Response> GetExample(List<string> keys);
    }
}
