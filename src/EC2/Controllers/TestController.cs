using Microsoft.AspNetCore.Mvc;
using EC2.Context; //NorthwindContext
using EC2.Models;
using EC2.Models.DTOs.Northwind;
using EC2.Repository;
using AutoMapper;

namespace EC2.Controllers
{
    /// <summary>
    /// A test controller for using EFCore
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public TestController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("/TESTAPI/Product/{id:int}")]
        public ProductResultVM Get(int id)
        {
            ///TODO: test generic automapper
            Product p = new Product()
            {
                ProductId = id,
                ProductName = "Test"
            };
            var testsource = new MapperTest<Product>()
            {
                Id = id,
                Data = p
            };
            var target = _mapper.Map<MapperTest<ProductResultVM>>(testsource);
            Console.WriteLine(target.GetType());
            ///
            var product = _productRepository.GetByID(id);
            return _mapper.Map<ProductResultVM>(product);
        }

        [HttpGet]
        [Route("/TESTAPI/Product/Search")]
        public PagedResultsVM<Product> GetPaging([FromQuery] ProductPagingVM parameters)
        {
            return _productRepository.GetPaging(parameters);
        }
    }
}
