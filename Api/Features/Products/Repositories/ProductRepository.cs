using Api.Features.Products.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Api.Features.Products.Repositories;

public class ProductRepository(ApplicationDbContext context, IMapper mapper)
    : GenericRepository<Product, ProductDto >(context, mapper), IProductRepository;
