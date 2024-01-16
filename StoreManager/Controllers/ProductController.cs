using Microsoft.AspNetCore.Mvc;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;

namespace StoreManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _repository.GetById(id);
        if (product == null)
            return NotFound(new ErrorMessage("Product not found"));
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Add(ProductDto productDto)
    {
        var product = _repository.Add(productDto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Product> Update([FromRoute] int id, [FromBody] ProductDto productDto)
    {
        try
        {
            var product = _repository.Update(id, productDto);
            return Ok(product);
        }
        catch (Exception e)
        {
            return NotFound(new ErrorMessage(e.Message));
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            _repository.Delete(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(new ErrorMessage(e.Message));
        }
    }
}