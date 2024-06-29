using System.Linq.Expressions;
using Api.Features.Products.DTOs;
using Api.Features.Products.Services;
using Api.Features.Products.specifications;
using AutoMapper;
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
        try
        {
            var response = await productService.GetListAsync(
                new ProductsWithBrandAndTypeSpecification(queries)
            );
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var response = await productService.GetByIdAsync
            (
                id,
                new BaseSpecification<Product, Product>()
            );
            return Ok(response);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
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