using Microsoft.AspNetCore.Mvc;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;

namespace StoreManager.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class SaleController : ControllerBase
{
    private readonly ISaleRepository _saleRepository;

    public SaleController(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    ///     Get all Sales
    /// </summary>
    /// <response code="200"> Returns all Sales </response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public ActionResult<IEnumerable<SaleProductDto>> GetAll()
    {
        return Ok(_saleRepository.GetAll());
    }

    /// <summary>
    ///     Get Sale by Id
    /// </summary>
    /// <param name="id">Sale Id</param>
    /// <response code="200"> Returns a Sale </response>
    /// <response code="404"> Sale not found </response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public ActionResult<IEnumerable<SaleProductDto>> GetById(int id)
    {
        var result = _saleRepository.GetById(id);
        return result == null ? NotFound(new ErrorMessage("Sale not found")) : Ok(result);
    }

    /// <summary>
    ///     Add a new Sale
    /// </summary>
    /// <param name="products"> Sale Products </param>
    /// <response code="201"> Returns the created Sale </response>
    /// <response code="400"> Invalid Products </response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public ActionResult<SaleOutputDto> Add(IEnumerable<SaleInputDto> products)
    {
        try
        {
            var result = _saleRepository.Add(products);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorMessage(e.Message));
        }
    }

    /// <summary>
    ///     Delete a Sale
    /// </summary>
    /// <param name="id">Sale Id</param>
    /// <response code="204">Sale Deleted</response>
    /// <response code="400">Invalid Sale Id</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            _saleRepository.Delete(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorMessage(e.Message));
        }
    }
}