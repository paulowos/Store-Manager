using Microsoft.EntityFrameworkCore;
using StoreManager.Context;
using StoreManager.Models;
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

    public IEnumerable<SaleProductDto>? GetById(int id)
    {
        var result = _context.SalesProducts
            .Include(sp => sp.Sale)
            .Where(sp => sp.SaleId == id)
            .Select(sp => new SaleProductDto
            {
                SaleId = sp.SaleId,
                Date = sp.Sale.Date,
                ProductId = sp.ProductId,
                Quantity = sp.Quantity
            });

        return !result.Any() ? null : result;
    }

    public IEnumerable<SaleProductDto> GetAll()
    {
        return _context.SalesProducts
            .Include(sp => sp.Sale)
            .Select(sp => new SaleProductDto
            {
                SaleId = sp.SaleId,
                Date = sp.Sale.Date,
                ProductId = sp.ProductId,
                Quantity = sp.Quantity
            });
    }

    public SaleOutputDto Add(IEnumerable<SaleInputDto> products)
    {
        var sale = new Sale
        {
            Date = DateTime.Now
        };

        _context.Sales.Add(sale);
        _context.SaveChanges();

        var saleId = sale.Id;

        var saleInputDtos = products as SaleInputDto[] ?? products.ToArray();
        foreach (var saleInputDto in saleInputDtos)
        {
            var product = _context.Products.Find(saleInputDto.ProductId);
            if (product == null)
            {
                _context.Sales.Remove(sale);
                _context.SaveChanges();
                throw new ArgumentException($"Product with id {saleInputDto.ProductId} does not exist");
            }

            _context.SalesProducts.Add(new SaleProduct
            {
                SaleId = saleId,
                ProductId = saleInputDto.ProductId,
                Quantity = saleInputDto.Quantity
            });
        }

        _context.SaveChanges();

        return new SaleOutputDto
        {
            Id = saleId,
            Date = sale.Date,
            ItemsSold = saleInputDtos
        };
    }
}