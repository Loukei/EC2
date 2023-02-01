﻿using Microsoft.AspNetCore.Mvc;
using EC2.Context; //NorthwindContext
using EC2.Models;
using EC2.Models.DTOs.Northwind;
using EC2.Repository;
using EC2.Service;
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
        private readonly NorthwindContext _northwindContext;
        private readonly IProductService _productService;

        public TestController(
            IMapper mapper, 
            IProductRepository productRepository,
            NorthwindContext northwindContext,
            IProductService productService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _northwindContext = northwindContext;
            _productService = productService;
        }

        [HttpGet]
        [Route("/TESTAPI/Product/{id:int}")]
        public ProductVM Get(int id)
        {
            var product = _productRepository.GetByID(id);
            return _mapper.Map<ProductVM>(product);
        }

        [HttpGet]
        [Route("/TESTAPI/Product/Search")]
        public PPagedList<ProductVM> GetPaging([FromQuery] ProductPageQueryVM parameters)
        {
            return _productService.GetPaging(parameters);
        }
    }
}
