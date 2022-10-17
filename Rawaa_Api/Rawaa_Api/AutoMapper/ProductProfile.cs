using AutoMapper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;

namespace Rawaa_Api.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // form ProductRequest to Product;
            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
            //.ForMember(dest => dest.Calories, opt => opt.Ignore())

            CreateMap<Product, ProductRequest>();
        }
    }
}
