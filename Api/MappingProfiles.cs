using Api.Features.Products.DTOs;
using AutoMapper;
using Core.Entities;

namespace Api;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));
        CreateMap<Product, Product>()
            .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));

        CreateMap<ProductBrand, ProductBrandDto>();
        CreateMap<ProductType, ProductTypeDto>();
        CreateMap<ProductType, ProductType>();
    }
}

