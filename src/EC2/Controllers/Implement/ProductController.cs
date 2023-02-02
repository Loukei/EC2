using Microsoft.AspNetCore.Mvc;
using EC2.Models;
using EC2.Service;
using EC2.Models.DTOs.Northwind;

namespace EC2.Controllers.Implement
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
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id:int:min(1)}")]
        public ProductAPIResultVM Get(int Id)
        {
            var response = new ProductAPIResultVM
            {
                Message = $"Get product {Id} has failed.",
                StatusCode = "Fail_001",
            };

            var product = _productService.Get(Id);
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
        [Route("All")]
        public ProductAPIResultVM GetAll([FromQuery] ProductPageQueryVM parameters)
        {
            var response = new ProductAPIResultVM
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
        //[Route("")]
        public ProductAPIResultVM Create(ProductUpdateVM parameters)
        {
            var response = new ProductAPIResultVM
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
        [Route("{Id:int:min(1)}")]
        public ProductAPIResultVM Update(int Id, ProductUpdateVM parameters)
        {
            var response = new ProductAPIResultVM
            {
                Message = $"Update product {Id} has failed.",
                StatusCode = "Fail_001",
            };

            var product = _productService.Update(Id, parameters);
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
        /// Delete single product by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{Id:int:min(1)}")]
        public ProductAPIResultVM Delete(int Id)
        {
            var response = new ProductAPIResultVM
            {
                Message = "Delete product has failed.",
                StatusCode = "Fail_001",
            };

            bool result = _productService.Delete(Id);
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
