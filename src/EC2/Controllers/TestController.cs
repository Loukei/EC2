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
        public ProductReplyVM Get(int id)
        {
            var product = _productRepository.GetByID(id);
            return _mapper.Map<ProductReplyVM>(product);
        }

        [HttpGet]
        [Route("/TESTAPI/Product/Search")]
        public PagedResultsVM<Product> GetPaging(string? name, int? supplierID, int? categoryID, int pageIndex = 1, int pageSize = 10)
        {
            return _productRepository.GetPaging(name, supplierID, categoryID, pageIndex, pageSize);
        }
    }
}
