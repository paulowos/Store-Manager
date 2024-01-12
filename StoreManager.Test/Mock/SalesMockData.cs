using StoreManager.Models;

namespace StoreManager.Test.Mock;

public static class SalesMockData
{
    public static Sale GetSale(int id)
    {
        return new Sale
        {
            Id = id,
            Date = default
        };
    }
}