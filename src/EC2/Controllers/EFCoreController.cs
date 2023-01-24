using Microsoft.AspNetCore.Mvc;
using EC2.Models.EFCore.Context;
using EC2.Models.EFCore;

namespace EC2.Controllers
{
    /// <summary>
    /// A test controller for using EFCore
    /// TODO: delete this controller after successfully change to EFcore
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EFCoreController : Controller
    {
        private readonly NorthwindContext _northwindContext;

        public EFCoreController(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        [HttpGet]
        [Route("/Product/{id:int}")]
        public Product Get(int id)
        {
            var product = _northwindContext.Products.Single(p => p.ProductId == id);

            return product;
        }
    }
}
