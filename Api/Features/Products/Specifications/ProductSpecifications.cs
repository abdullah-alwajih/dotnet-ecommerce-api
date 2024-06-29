using System.Linq.Expressions;
using Api.Features.Products.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace Api.Features.Products.specifications;

public class ProductsWithBrandAndTypeSpecification(BaseQueries queries)
    : BaseSpecification<Product>(
        criteria: ProductSpecifications.DefaultCriteria(queries),
        pagination: queries);

public abstract class ProductSpecifications
{
    public static Expression<Func<Product, bool>> DefaultCriteria(BaseQueries queries)
    {
        return p => (string.IsNullOrEmpty(queries.Name) ||
                     (p.ProductBrand != null && p.ProductBrand.Name.Contains(queries.Name))) &&
                    (!queries.Price.HasValue || p.Price < queries.Price.Value);
    }


    public static Expression<Func<Product, ProductDto>> ToProductDto(IMapper mapper)
    {
        return product => new ProductDto

        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ProductBrand = product.ProductBrand == null
                ? null
                : mapper.Map<ProductBrand, ProductBrandDto>(product.ProductBrand),
            ProductType = product.ProductType == null
                ? null
                : mapper.Map<ProductType, ProductTypeDto>(product.ProductType)
        };
    }
}