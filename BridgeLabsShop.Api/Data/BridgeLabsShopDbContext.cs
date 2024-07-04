using BridgeLabsShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BridgeLabsShop.Api.Data
{
  
    public class BridgeLabsShopDbContext : DbContext
    {
        public BridgeLabsShopDbContext(DbContextOptions<BridgeLabsShopDbContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
 
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
