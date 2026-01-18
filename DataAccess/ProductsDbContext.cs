using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
