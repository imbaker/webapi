using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApi.Extensions
{
    public static class HttpActionResultExtensions
    {
        public static IHttpActionResult OkWithLocation<T>(this ApiController controller, HttpStatusCode statusCode, T content)
        {
            return new NegotiatedContentResult<T>(statusCode, content, controller);
        }

        public static IHttpActionResult AddHeader(this IHttpActionResult action, string headerKey, string headerValue)
        {
            HttpContext.Current.Response.Headers.Add(headerKey, headerValue);
            return action;
        }
    }
}