using Microsoft.EntityFrameworkCore;
using StoreManager.Context;

namespace StoreManager.Test.Context;

public class DatabaseContext
{
    public StoreManagerContext GetContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<StoreManagerContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
        var context = new StoreManagerContext(options);
        return context;
    }
}