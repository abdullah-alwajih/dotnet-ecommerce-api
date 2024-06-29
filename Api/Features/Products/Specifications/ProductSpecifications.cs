using System.Linq.Expressions;
using Api.Features.Products.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace Api.Features.Products.specifications;

public class ProductsWithBrandAndTypeSpecification(BaseQueries queries)
    : BaseSpecification<Product, ProductDto>(
        criteria: ProductSpecifications.DefaultCriteria(queries),
        select: ProductSpecifications.ToProductDto,
        pagination: queries);

public abstract class ProductSpecifications
{
    public static Expression<Func<Product, bool>> DefaultCriteria(BaseQueries queries)
    {
        return p => (string.IsNullOrEmpty(queries.Name) ||
                     (p.ProductBrand != null && p.ProductBrand.Name.Contains(queries.Name))) &&
                    (!queries.Price.HasValue || p.Price < queries.Price.Value);
    }

    
    
    public static Expression<Func<Product, ProductDto>> ToProductDto => product => new ProductDto
        
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        ProductBrand = product.ProductBrand == null ? null : new ProductBrandDto
        {
            Id = product.ProductBrand.Id,
            Name = product.ProductBrand.Name
        },
        ProductType = product.ProductType == null ? null : new ProductTypeDto
        {
            Id = product.ProductType.Id,
            Name = product.ProductType.Name
        }
    };

}