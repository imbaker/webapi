using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [RoutePrefix("api/v1/concerts")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConcertController : ApiController
    {
        private readonly IRepository<Concert> repository;

        public ConcertController() { }

        public ConcertController(IRepository<Concert> repository)
        {
            this.repository = repository;
        }

        [Route("{id}")]
        public IHttpActionResult GetConcert(int id)
        {
            var concert = repository.GetById(id);
            if (concert == null)
            {
                return NotFound();
            }

            return new CustomOkResult<Concert>(concert, this) { ETagValue = Encode(concert.LastUpdated.ToString()) };
        }

        private string Encode(string etag)
        {
            var data = System.Text.UTF8Encoding.UTF8.GetBytes(etag);
            return System.Convert.ToBase64String(data);
        }
    }
}
