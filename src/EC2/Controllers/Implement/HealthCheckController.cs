using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using EC2.Models;

namespace EC2.Controllers.Implement
{
    /// <summary>
    /// A Simple Controller to check server healthy
    /// </summary>
    [Route("api/healthcheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }
    }
}
