using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Api.Features.Products.Repositories;

public class ProductTypesRepository(ApplicationDbContext context)
    : BaseRepository<ProductType>(context), IProductTypesRepository
{
}