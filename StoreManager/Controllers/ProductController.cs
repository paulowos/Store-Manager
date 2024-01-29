using Microsoft.AspNetCore.Mvc;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;

namespace StoreManager.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    ///     Get all Products
    /// </summary>
    /// <response code="200">Returns all Products</response>
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    /// <summary>
    ///     Get Product by Id
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <response code="200">Returns a Product</response>
    /// <response code="404">Product not found</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _repository.GetById(id);
        if (product == null)
            return NotFound(new ErrorMessage("Product not found"));
        return Ok(product);
    }

    /// <summary>
    ///     Add a new Product
    /// </summary>
    /// <param name="productDto"> Product Name</param>
    /// <response code="201"> Returns the created Product </response>
    /// <response code="400"> Invalid Product Name </response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public ActionResult<Product> Add(ProductDto productDto)
    {
        var product = _repository.Add(productDto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    /// <summary>
    ///     Update a Product
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <param name="productDto">Product Name</param>
    /// <response code="200"> Returns the updated Product </response>
    /// <response code="404"> Product not found </response>
    /// <response code="400"> Invalid Product Name </response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    ///     Delete a Product
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <response code="204"> Product deleted </response>
    /// <response code="404"> Product not found </response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
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

    /// <summary>
    ///     Search Products by Name
    /// </summary>
    /// <param name="q">Product Name</param>
    /// <response code="200"> Returns Products </response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("search")]
    public ActionResult<IEnumerable<Product>> Search([FromQuery] string? q = "")
    {
        return Ok(_repository.Search(q));
    }
}