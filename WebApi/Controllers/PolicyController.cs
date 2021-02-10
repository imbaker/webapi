using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;
using AutoMapper;
using Swashbuckle.Swagger.Annotations;
using WebApi.Enums;
using WebApi.Extensions;
using WebApi.Models.Domain;
using WebApi.Models.Dto;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/v1/policies")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PolicyController : ApiController
    {
        private readonly IJsonRepository repository;
        private readonly INewsRepository newsRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        public PolicyController()
        {
        }

        public PolicyController(IJsonRepository repository, INewsRepository newsRepository, IMapper mapper)
        {
            this.repository = repository;
            this.newsRepository = newsRepository;
            this.mapper = mapper;
        }

        [Route("")]
        public IEnumerable<Policy> GetAllProducts()
        {
            var policyDto = new PolicyDto { Id = 1, LastUpdated = DateTime.Now, PolicyNo = "PN1", Firstname = "Ian", Surname = "Baker" };
            Policy policies = mapper.Map<PolicyDto, Policy>(policyDto);
            yield return policies;
        }

        [SwaggerOperation("GetConcert")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Bad Request")]
        [Route("{id}")]
        public IHttpActionResult GetConcert(int id)
        {
            var policy = repository.GetPolicy(id);
            if (policy == null)
            {
                return NotFound();
            }

            var policyDto = mapper.Map<Policy, PolicyDto>(policy);

            return new CustomOkResult<PolicyDto>(policyDto, this) { ETagValue = Hash(policyDto.LastUpdated.ToString()) };
        }

        [HttpPost]
        [ResponseType(typeof(PolicyDto))]
        [Route("")]
        public IHttpActionResult PostConcert(PolicyDto policyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var policy = mapper.Map<PolicyDto, Policy>(policyDto);

            var newId = repository.AddPolicy(policy);

            var response = repository.GetPolicy(newId);
            var location = $"{Request.RequestUri}/{newId}";
            return Created(location, mapper.Map<Policy, PolicyDto>(response));
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult PutConcert([FromUri] int id, [FromBody] PolicyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var originalPolicy = repository.GetPolicy(id);
            if (originalPolicy == null)
                return NotFound();

            var etag = new EntityTagHeaderValue($"\"{Hash(originalPolicy.LastUpdated.ToString())}\"");

            if (Request.Headers.IfMatch.ToString() != etag.ToString())
            {
                return Content<string>(HttpStatusCode.PreconditionFailed, "If-Match value is no longer valid");
            }

            var policy = mapper.Map<PolicyDto, Policy>(dto);
            var response = repository.UpdatePolicy(id, policy);
            if (response == null)
                return NotFound();

            var updatedDto = mapper.Map<Policy, PolicyDto>(response);

            return new CustomOkResult<PolicyDto>(updatedDto, this) { ETagValue = Hash(updatedDto.LastUpdated.ToString()) };
        }

        [HttpGet]
        [Route("news/{searchParameter}")]
        public async Task<IHttpActionResult> GetNews([FromUri] string searchParameter)
        {
            var content = await newsRepository.Search(searchParameter);
            if (content.Status.Equals("OK", StringComparison.OrdinalIgnoreCase))
                return Ok(content);

            return NotFound();
        }

        [HttpGet]
        [Route("id")]
        public async Task<IHttpActionResult> GetId([FromUri] string id)
        {
            var content = await newsRepository.Get(id);
            if (content.Status.Equals("OK", StringComparison.OrdinalIgnoreCase))
                return Ok(content.Content);

            return NotFound();
        }

        [HttpGet]
        [Route("Example/{id}")]
        public IHttpActionResult GetExample([FromUri] int id, [FromUri] string assignedTo)
        {
            var assignedToItems = assignedTo.Split(',');

            var keys = new List<string>();
            ValidAssignedTo assignedToFlag = 0;
            foreach (var item in assignedToItems)
            {
                if (!Enum.TryParse<ValidAssignedTo>(item, out var value))
                    return BadRequest($"Invalid assignedTo value: {item}");

                assignedToFlag |= value;
            }

            if (assignedToFlag.HasFlag(ValidAssignedTo.CurrentUser | ValidAssignedTo.Workgroup))
            {
                return new OkNegotiatedContentResult<string>("Both are set", this);
            }

            if (assignedToFlag.HasFlag(ValidAssignedTo.CurrentUser))
            {
                return new OkNegotiatedContentResult<string>("CurrentUser is set", this);
            }

            if (assignedToFlag.HasFlag(ValidAssignedTo.Workgroup))
            {
                return new OkNegotiatedContentResult<string>("Workgroup is set", this);
            }

            var response = newsRepository.GetExample(keys);

            return NotFound();
        }

        private static string Hash(string value)
        {
            var hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(Encoding.UTF8.GetBytes(value));
            var result = new StringBuilder();
            foreach (byte i in hash)
            {
                result.Append(i.ToString("x2"));
            }
            return result.ToString();
        }
    }
}