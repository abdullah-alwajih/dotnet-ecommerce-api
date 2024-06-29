using Api.Features.Products.DTOs;
using Api.Features.Products.Repositories;
using Core.Entities;
using Core.Interfaces;

namespace Api.Features.Products.Services;

public class ProductBrandsService(IProductBrandsRepository productBrandsRepository)
    : BaseService<ProductBrand, ProductBrandDto>(productBrandsRepository), IProductBrandsService
{
}