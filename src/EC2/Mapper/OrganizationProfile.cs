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
            ///TODO: CategoryName should not empty when categoryId exists
            CreateMap<Product, ProductReplyVM>()
                .ForMember(dest => dest.CategoryName, 
                    opt => opt.MapFrom(src => (src.Category != null) ? src.Category.CategoryName : string.Empty));            
        }
    }
}
