using DataAccess;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class CleanupFunction(
    ILogger<CleanupFunction> logger,
    ProductsDbContext dbContext)
{
    [Function("CleanupFunction")]
    public async Task Run([TimerTrigger("0 0 */4 * * *")] TimerInfo myTimer)
    {
        var products = dbContext.Products.ToList();

        if (products.Count == 0)
        {
            logger.LogInformation("Skip cleanup, as there are no products found");
            return;
        }

        dbContext.Products.RemoveRange(products);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Cleaned {Count} products", products.Count);
    }
}