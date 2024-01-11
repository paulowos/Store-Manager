using Microsoft.AspNetCore.Mvc;
using StoreManager.Models;
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
    public ActionResult<Product> Get(int id)
    {
        var product = _repository.GetById(id);
        if (product == null)
            return NotFound(new ErrorMessage("Product not found"));
        return Ok(product);
    }
}