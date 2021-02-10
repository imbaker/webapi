using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;

namespace WebApi.Controllers
{
    [RoutePrefix("api/v1/pages")]
    public class PageController : ApiController
    {
        [Route("{id}")]
        public async Task<IHttpActionResult> GetPages(int? id = null)
        {
            using (var context = new AngularDbContext())
            {
                var pages = await context.Pages.Where(p => p.Id == id || id == null).ToListAsync().ConfigureAwait(false);

                return Ok(pages.Select(p => new {
                    p.Title,
                    p.Content
                }));
            };
        }
    }

}
