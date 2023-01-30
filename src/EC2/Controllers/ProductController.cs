using Microsoft.AspNetCore.Mvc;
using EC2.Models;
using EC2.Service;
using EC2.Models.DTOs.Northwind;

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
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Product/{productId:int:min(1)}")]
        public ProductServiceResponse Get(int productId)
        {
            var response = new ProductServiceResponse
            {
                Message = $"Get product {productId} has failed.",
                StatusCode = "Fail_001",
            };

            var product = _productService.Get(productId);
            if (product != null)
            {
                response.IsSucessful = true;
                response.Result = product;
                response.Message = "Get Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }

        //product? pageindex = 1 & pagesize = 10 & name = abc & categoryid = 123
        [HttpGet]
        [Route("/Product/all")]
        public ProductServiceResponse GetAll([FromQuery] ProductPagingVM parameters)
        {
            var response = new ProductServiceResponse
            {
                Message = $"GetAll products  has failed.",
                StatusCode = "Fail_001",
            };

            var prods = _productService.GetPaging(parameters);

            if (prods != null)
            {
                response.IsSucessful = true;
                response.Result = prods;
                response.Message = "GetAll Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }

        /// <summary>
        /// Create single product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/Product")]
        public ProductServiceResponse Create(ProductRequestVM parameters)
        {
            var response = new ProductServiceResponse
            {
                Message = "Create product has failed.",
                StatusCode = "Fail_001",
            };

            var prod = _productService.Create(parameters);
            if (prod != null)
            {
                response.IsSucessful = true;
                response.Result = prod;
                response.Message = "Create Successfully";
                response.StatusCode = "Success";
            }
            return response;
        }

        [HttpPut]
        [Route("update/{productId:int:min(1)}")]
        public ProductServiceResponse Update(int productId,ProductRequestVM parameters)
        {
            var response = new ProductServiceResponse
            {
                Message = $"Update product {productId} has failed.",
                StatusCode = "Fail_001",
            };

            var product = _productService.Update(productId, parameters);
            if (product != null)
            {
                response.IsSucessful = true;
                response.Result = product;
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
        public ProductServiceResponse Delete(int productId)
        {
            var response = new ProductServiceResponse
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
