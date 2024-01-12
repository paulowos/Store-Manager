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
    public ActionResult<IEnumerable<SaleDto>> GetAll()
    {
        return Ok(_saleRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<IEnumerable<SaleDto>> GetById(int id)
    {
        var result = _saleRepository.GetById(id);
        return result == null ? NotFound(new ErrorMessage("Sale not found")) : Ok(result);
    }
}