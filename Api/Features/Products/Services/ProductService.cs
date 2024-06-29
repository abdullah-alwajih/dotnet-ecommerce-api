using Api.Features.Products.DTOs;
using Api.Features.Products.Repositories;
using Core.Entities;
using Core.Interfaces;

namespace Api.Features.Products.Services;

public class ProductService(IProductRepository productRepository)
    : BaseService<Product, ProductDto>(productRepository), IProductService
{
}