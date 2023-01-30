using AutoMapper;
using EC2.Models.DTOs.Northwind;
using EC2.Models;

namespace EC2.Mapper
{
    /// <summary>
    /// An class for automapper
    /// </summary>
    public class OrganizationProfile: Profile
    {
        public OrganizationProfile()
        {
            CreateMap<ProductRequestVM, Product>();

            CreateMap<Product, ProductResultVM>()
                .ForMember(dest => dest.CategoryName, 
                    opt => opt.MapFrom(src => (src.Category != null) ? src.Category.CategoryName : string.Empty))
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => (src.Supplier != null) ? src.Supplier.CompanyName : string.Empty));
            /// TODO: mapping PagedResultsVM<ProductResultVM>
            /// <see cref="Service.Implement.ProductService.GetPaging(ProductPagingVM)"/>            
            CreateMap(typeof(PagedResultsVM<>), typeof(PagedResultsVM<>));
            /// TODO: TEST genreic mapper
            CreateMap(typeof(MapperTest<>), typeof(MapperTest<>));
        }
    }
}
