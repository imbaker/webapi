using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using WebApi.Models.Domain.Guardian;

namespace WebApi.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly HttpClient httpClient;

        public NewsRepository() { }

        public NewsRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Response> Search(string searchParameter)
        {
            var response = await "https://content.guardianapis.com"
                .AppendPathSegment("search")
                .WithHeader("api-key", "e46b337f-b283-448d-8a64-64d7da8dd011")
                .SetQueryParams(new { q = searchParameter })
                .GetJsonAsync<Body>();
            return response.Response;
        }

        public async Task<Response> Get(string id)
        {
            try
            {
                var response = await "https://content.guardianapis.com"
                    .AppendPathSegment(id)
                    .WithHeader("api-key", "e46b337f-b283-448d-8a64-64d7da8dd011")
                    .GetJsonAsync<Body>();

                return response.Response;
            }
            catch (FlurlHttpException e)
            {
                var error = await e.GetResponseJsonAsync();
                throw;
            }
        }

        public async Task<Response> GetExample(List<string> keys)
        {
            try
            {
                var response = await "https://content.guardianapis.com"
                    .WithHeader("api-key", "e46b337f-b283-448d-8a64-64d7da8dd011")
                    .GetJsonAsync<Body>();

                return response.Response;
            }
            catch (FlurlHttpException e)
            {
                var error = await e.GetResponseJsonAsync();
                throw;
            }
        }
    }
}