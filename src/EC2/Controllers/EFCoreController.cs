using Microsoft.AspNetCore.Mvc;
using EC2.Context; //NorthwindContext
using EC2.Models;
using EC2.Models.DTOs.Northwind;

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

        [HttpGet]
        [Route("/Product/Search")]
        public ProductPagingResponseModel GetPaging(string? name, int? supplierID, int? categoryID, int pageIndex, int pageSize)
        {
            /// 
            var queryStatement = _northwindContext.Products
                .Where(p => p.Status == true
                    && (name == null || p.ProductName == name)
                    && (supplierID == null || p.SupplierId == supplierID)
                    && (categoryID == null || p.CategoryId == categoryID));
            int totalRecords = queryStatement.Count();
            int totalPages = Convert.ToInt32(Math.Ceiling(((double)totalRecords / (double)pageSize)));
            var records = queryStatement
                .OrderBy(p => p.ProductId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            return new ProductPagingResponseModel(records, totalRecords, pageIndex, pageSize, totalPages);

        }
    }
}
