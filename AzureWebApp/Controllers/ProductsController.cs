using AzureWebApp.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace AzureWebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(ILogger<ProductsController> logger) : ControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> ListProducts()
    {
        var products = new Faker<Product>()
            .RuleFor(x => x.Id, x => x.Random.Uuid())
            .RuleFor(x => x.Name, x => x.Commerce.ProductName())
            .RuleFor(x => x.Description, x => x.Commerce.ProductDescription())
            .RuleFor(x => x.Price, x => x.Commerce.Price(10, 200))
            .RuleFor(x => x.Quantity, x => x.Random.Int(1, 100))
            .Generate(10);

        logger.LogInformation("Generated {Count} products", products.Count);

        return Ok(products);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreationRequest request)
    {
        var productId = Guid.NewGuid();

        var newProduct = new Product
        {
            Id = productId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };

        logger.LogInformation("Created new product with ID: {ProductId}", newProduct.Id);

        return CreatedAtAction(nameof(CreateProduct), new { id = newProduct.Id }, newProduct);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteProduct([FromBody] string productId)
    {
        logger.LogInformation("Deleted product with ID: {ProductId}", productId);
        return NoContent();
    }
}
