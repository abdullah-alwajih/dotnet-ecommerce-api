using Api.Features.Products.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Api.Features.Products.Repositories;

public class ProductBrandsRepository(ApplicationDbContext context, IMapper mapper)
    : GenericRepository<ProductBrand, ProductBrandDto>(context, mapper), IProductBrandsRepository;
