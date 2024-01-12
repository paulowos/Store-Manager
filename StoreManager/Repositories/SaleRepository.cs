using Microsoft.EntityFrameworkCore;
using StoreManager.Context;
using StoreManager.Models.Dto;
using StoreManager.Models.Interfaces;

namespace StoreManager.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly StoreManagerContext _context;

    public SaleRepository(StoreManagerContext context)
    {
        _context = context;
    }

    public IEnumerable<SaleDto>? GetById(int id)
    {
        var result = _context.SalesProducts
            .Include(sp => sp.Sale)
            .Where(sp => sp.SaleId == id)
            .Select(sp => new SaleDto
            {
                SaleId = sp.SaleId,
                Date = sp.Sale.Date,
                ProductId = sp.ProductId,
                Quantity = sp.Quantity
            });

        return !result.Any() ? null : result;
    }

    public IEnumerable<SaleDto> GetAll()
    {
        return _context.SalesProducts
            .Include(sp => sp.Sale)
            .Select(sp => new SaleDto
            {
                SaleId = sp.SaleId,
                Date = sp.Sale.Date,
                ProductId = sp.ProductId,
                Quantity = sp.Quantity
            });
    }
}