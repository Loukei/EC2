using AutoMapper;
using NorthWindEFLibrary.DTOs;
using EC2.Models;

namespace EC2.Mapper
{
    /// <summary>
    /// An class for automapper profile
    /// </summary>
    public class OrganizationProfile: Profile
    {
        public OrganizationProfile()
        {
            CreateMap<ProductUpdateVM, Product>();

            CreateMap<Product, ProductVM>()
                .ForMember(
                    dest => dest.CategoryName, 
                    opt => opt.MapFrom(
                        src => (src.Category != null) ? src.Category.CategoryName : string.Empty))
                .ForMember(
                    dest => dest.SupplierName,
                    opt => opt.MapFrom(
                        src => (src.Supplier != null) ? src.Supplier.CompanyName : string.Empty));
        }
    }
}
