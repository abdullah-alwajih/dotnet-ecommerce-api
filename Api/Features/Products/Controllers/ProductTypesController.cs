using System.Linq.Expressions;
using Api.Features.Products.DTOs;
using Api.Features.Products.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTypesController(IProductTypesService productTypesService) : ControllerBase
{
    // GET: api/<ProductTypesController>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BaseQueries queries)
    {
        var productTypes = await productTypesService.GetListAsync(
           new BaseSpecification<ProductType, ProductType>(pagination: queries)
        );
        return Ok(productTypes);
    }


    // GET api/<ProductTypesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductTypesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ProductTypesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductTypesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}