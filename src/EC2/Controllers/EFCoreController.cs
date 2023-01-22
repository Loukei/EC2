using Microsoft.AspNetCore.Mvc;
using EC2.Models.EFcore;

namespace EC2.Controllers
{
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
        [Route("/{id:int}")]
        public ActionResult Get(int id)
        {
            var product = _northwindContext.Products.Single(p => p.ProductId == id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
