using Microsoft.AspNetCore.Mvc;
using StoreManager.Models;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;

namespace StoreManager.Controllers;

[ApiController]
[Route("[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleRepository _saleRepository;

    public SaleController(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SaleProductDto>> GetAll()
    {
        return Ok(_saleRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<IEnumerable<SaleProductDto>> GetById(int id)
    {
        var result = _saleRepository.GetById(id);
        return result == null ? NotFound(new ErrorMessage("Sale not found")) : Ok(result);
    }

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
}