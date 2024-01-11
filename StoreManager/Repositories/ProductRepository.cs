using StoreManager.Context;

namespace StoreManager.Repositories;

public class ProductRepository
{
    private StoreManagerContext _context;

    public ProductRepository(StoreManagerContext context)
    {
        _context = context;
    }
}