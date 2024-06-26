﻿using Api.Features.Products.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Api.Features.Products.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
}