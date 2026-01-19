using DataAccess;
using DataAccess.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AzureFunctions;

public class AddProductsFunction(
    ILogger<AddProductsFunction> logger,
    ProductsDbContext dbContext)
{
    [Function("AddProductsFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData request)
    {
        var products = await request.ReadFromJsonAsync<Product[]>();

        if (products is null || products.Length == 0)
        {
            logger.LogWarning("No products were provided in the request.");
            var badResponse = request.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Please pass a valid products");
            return badResponse;
        }

        dbContext.Products.AddRange(products);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Added {Count} products to the database.", products.Length);

        var response = request.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(products);
        return response;
    }
}