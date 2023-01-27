using Microsoft.AspNetCore.Mvc;
using EC2.Models;
using EC2.Service;
using EC2.Models.EFCore;

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

        //product? pageindex = 1 & pagesize = 10 & name = abc & categoryid = 123
        [HttpGet]
        public ProductServiceResponse GetAll([FromQuery] ProductPagingViewModel parameters)
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
        public ProductServiceResponse Create(ProductViewModel product)
        {
            var response = new ProductServiceResponse
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/{productId:int:min(1)}")]
        public ProductServiceResponse Get(int productId)
        {            
            var response = new ProductServiceResponse
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
        public ProductServiceResponse Update(int productId,ProductViewModel product)
        {
            var response = new ProductServiceResponse
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
