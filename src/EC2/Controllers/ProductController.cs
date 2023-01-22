using Microsoft.AspNetCore.Mvc;
//using System.Net;
using EC2.Models;
using EC2.Models.EFcore;
using EC2.Service;

namespace EC2.Controllers
{

    /// <summary>
    /// A service to maintain Product
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Create single product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResponse Create(ProductViewModel product)
        {
            var response = new ServiceResponse
            {
                Message = "Create product has failed.",
                StatusCode = "Fail_001",
            };

            var prod = _productService.Create(product);
            if (prod != null)
            {
                response.IsSucessful = true;
                response.Result = prod;
                response.Message = "Create Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }

        //product? pageindex = 1 & pagesize = 10 & name = abc & categoryid = 123
        [HttpGet]
        public ServiceResponse GetAll(string? name, int? supplierID, int? categoryID, int pageIndex = 1, int pageSize = 10)
        {
            var response = new ServiceResponse
            {
                Message = $"GetAll products  has failed.",
                StatusCode = "Fail_001",
            };

            var prods = _productService.GetAll(name, supplierID, categoryID, pageIndex, pageSize);
            if (prods != null)
            {
                response.IsSucessful = true;
                response.Result = prods;
                response.Message = "GetAll Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }

        //[HttpGet]
        //public ServiceResponse GetPaging(string? name, int? supplierID, int? categoryID, int pageIndex = 1, int pageSize = 10)
        //{
        //    /// TODO: ERROR WHEN ADD FUNCTION
        //    var response = new ServiceResponse
        //    {
        //        Message = $"Paging products  has failed.",
        //        StatusCode = "Fail_001",
        //    };

        //    var pagingResults = _productService.GetPaging(name, supplierID, categoryID, pageIndex, pageSize);
        //    if (pagingResults != null)
        //    {
        //        response.IsSucessful = true;
        //        response.Result = pagingResults;
        //        response.Message = "Paging Successfully";
        //        response.StatusCode = "Success";
        //    }
        //    return response;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/{productId:int:min(1)}")]
        public ServiceResponse Get(int productId)
        {            
            var response = new ServiceResponse
            {
                Message = $"Get product {productId} has failed.",
                StatusCode = "Fail_001",
            };

            Product prod = _productService.Get(productId);
            if (prod != null)
            {
                response.IsSucessful = true;
                response.Result = prod;
                response.Message = "Get Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }

        [HttpPut]
        [Route("update/{productId:int:min(1)}")]
        public ServiceResponse Update(int productId,ProductViewModel product)
        {
            var response = new ServiceResponse
            {
                Message = $"Update product {productId} has failed.",
                StatusCode = "Fail_001",
            };

            Product prod = _productService.Update(productId, product);
            if (prod != null)
            {
                response.IsSucessful = true;
                response.Result = prod;
                response.Message = "Update Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }

        /// <summary>
        /// Delete single product by productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/{productId:int}")]
        public ServiceResponse Delete(int productId)
        {
            var response = new ServiceResponse
            {
                Message = "Delete product has failed.",
                StatusCode = "Fail_001",
            };

            bool result = _productService.Delete(productId);
            if (result)
            {
                response.IsSucessful = true;
                response.Result = result;
                response.Message = "Delete Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }
    }
}
