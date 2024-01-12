using Microsoft.AspNetCore.Mvc;
using StoreManager.Models;
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
    public IActionResult GetAll()
    {
        return Ok(_saleRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _saleRepository.GetById(id);
        return result == null ? NotFound(new ErrorMessage("Sale not found")) : Ok(result);
    }
}