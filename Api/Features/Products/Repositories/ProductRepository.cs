using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Api.Features.Products.Repositories;

public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product>(context), IProductRepository
{
}