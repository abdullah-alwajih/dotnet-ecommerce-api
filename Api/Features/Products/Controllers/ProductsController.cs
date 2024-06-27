using System.Linq.Expressions;
using Api.Features.Products.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Features.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BaseQueries queries)
    {
        Expression<Func<Product, bool>>? predicate = null;

        if (!string.IsNullOrEmpty(queries.Name)) predicate = p => p.ProductBrand.Name.Contains(queries.Name);

        var products = await productService.GetListAsync(
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
            queries
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