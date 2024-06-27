using System.Linq.Expressions;
using Api.Features.Products.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Features.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService _productService) : ControllerBase
{
    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string? name = null)
    {
        Expression<Func<Product, bool>>? predicate = null;

        if (!string.IsNullOrEmpty(name)) predicate = p => p.Name.Contains(name);

        var products = await _productService.GetListAsync(
            predicate,
            product => new
            {
                product.Name,
                product.Price,
                ProductBrand = product.ProductBrand == null
                    ? null
                    : new
                    {
                        product.ProductBrand.Id, product.ProductBrand.Name
                    }
            },
            pageNumber,
            pageSize
        );
        return Ok(products);
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }


    // PATCH api/<ProductsController>/5
    [HttpPatch("{id}")]
    public void Patcht(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}