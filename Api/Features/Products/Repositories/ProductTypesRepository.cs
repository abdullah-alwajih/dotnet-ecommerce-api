using Api.Features.Products.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Api.Features.Products.Repositories;

public class ProductTypesRepository(ApplicationDbContext context, IMapper mapper)
    : GenericRepository<ProductType, ProductTypeDto>(context, mapper), IProductTypesRepository;
