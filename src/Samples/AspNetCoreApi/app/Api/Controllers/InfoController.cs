using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiTemplate.Api.Controllers
{
    /// <summary>
    /// temporary controller to provide feedback while building out-of-process component tests
    /// </summary>
    public class InfoController : ApiController
    {
        private readonly IConfiguration _configuration;

        public InfoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return _configuration.GetConnectionString("AppDb");
        }
    }
}