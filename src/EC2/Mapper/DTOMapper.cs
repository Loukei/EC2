using EC2.Models.DTOs.Northwind;
using EC2.Models;

using AutoMapper;

namespace EC2.Mapper
{
    /// <summary>
    /// 負責存放大量的"將資料型態A轉成資料型態B"的函數
    /// 何時需要修改程式碼: 當DTO或ViewModel資料被新增或刪除的時候需要刪除對應的函數
    /// </summary>
    public static class DTOMapper
    {
        /// <summary>
        /// In <see cref="Repository.ProductRepository"/>, turn query result to <see cref="Models.ProductReplyVM"/>
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductReplyVM ToProductReplyVM(this Product product)
        {
            /// TODO: a nullable return
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductReplyVM>())
                .CreateMapper();
            var vm = mapper.Map<ProductReplyVM>(product);
            return vm;
        }

        /// <summary>
        /// In <see cref="Repository.ProductRepository.Create(ProductRequestVM)"/>, turn parameter to product
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static Product ToProduct(this ProductRequestVM parameter)
        {
            /// TODO: a nullable return
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductRequestVM, Product>())
                .CreateMapper();
            var product = mapper.Map<Product>(parameter);
            return product;
        }

        public static List<ProductReplyVM> ToProductReplyVMList(this List<Product> products)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductReplyVM>()).CreateMapper();
            var results = mapper.Map<List<Product>, List<ProductReplyVM>>(products);
            return results;
        }
    }
}
