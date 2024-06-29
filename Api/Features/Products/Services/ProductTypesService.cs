using Api.Features.Products.DTOs;
using Api.Features.Products.Repositories;
using Core.Entities;
using Core.Interfaces;

namespace Api.Features.Products.Services;

public class ProductTypesService(IProductTypesRepository productTypesRepository)
    : BaseService<ProductType>(productTypesRepository), IProductTypesService
{
}