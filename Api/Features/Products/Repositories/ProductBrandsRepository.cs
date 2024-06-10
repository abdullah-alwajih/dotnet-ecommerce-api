using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Api.Features.Products.Repositories;

public class ProductBrandsRepository(ApplicationDbContext context)
    : BaseRepository<ProductBrand>(context), IProductBrandsRepository
{
}