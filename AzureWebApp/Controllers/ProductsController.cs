using AzureWebApp.Models;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AzureWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(
    ILogger<ProductsController> logger,
    ProductsDbContext dbContext) : ControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> ListProducts()
    {
        var products = dbContext.Products.ToList();

        if (products.Count == 0)
        {
            logger.LogInformation("No products found");
            return NotFound();
        }

        logger.LogInformation("Found {Count} products", products.Count);
        return Ok(products);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreationRequest request)
    {
        var newProduct = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };

        dbContext.Products.Add(newProduct);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Created new product with ID: {ProductId}", newProduct.Id);

        return CreatedAtAction(nameof(CreateProduct), new { id = newProduct.Id }, newProduct);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteProduct([FromBody] Guid productId)
    {
        var product = await dbContext.Products.FindAsync(productId);
        if (product == null)
        {
            logger.LogWarning("Product with ID: {ProductId} not found for deletion", productId);
            return NotFound();
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Deleted product with ID: {ProductId}", productId);
        return NoContent();
    }
}
