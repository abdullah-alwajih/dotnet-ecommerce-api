using Core.Entities;

namespace Api.Features.Products.DTOs;

public class ProductDto
{
    public int Id { get; set; }

    public string? ImageUrl { get; set; }

    public required string Name { get; set; }
    public decimal Price { get; set; }
    public ProductBrandDto? ProductBrand { get; set; }
    public ProductTypeDto? ProductType { get; set; }
}