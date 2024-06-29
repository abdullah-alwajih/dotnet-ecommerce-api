using System.Linq.Expressions;
using Api.Features.Products.DTOs;
using Api.Features.Products.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Features.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductBrandsController(IProductBrandsService productBrandsService) : ControllerBase
{
    private readonly IProductBrandsService _productBrandsService = productBrandsService;


    // GET: api/<ProductBrandsController>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BaseQueries queries)
    {
       
        var productBrands = await _productBrandsService.GetListAsync(
            new BaseSpecification<ProductBrand, ProductBrand>(pagination: queries)
        );
        return Ok(productBrands);
    }

    // GET api/<ProductBrandsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductBrandsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ProductBrandsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductBrandsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}