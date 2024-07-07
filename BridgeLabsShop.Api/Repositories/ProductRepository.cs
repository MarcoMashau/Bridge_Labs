using BridgeLabsShop.Api.Data;
using BridgeLabsShop.Api.Entities;
using BridgeLabsShop.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BridgeLabsShop.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BridgeLabsShopDbContext bridgeLabsShopDbContext;        

        public ProductRepository(BridgeLabsShopDbContext bridgeLabsShopDbContext)
        {
            this.bridgeLabsShopDbContext = bridgeLabsShopDbContext;
        }


        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.bridgeLabsShopDbContext.Products.ToArrayAsync();
            return products;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
           var categories = await this.bridgeLabsShopDbContext.ProductCategories.ToArrayAsync();
            return categories;  
        }

        public async Task<Product> GetItem(int id)
        {
          var product = await bridgeLabsShopDbContext.Products.FindAsync(id);
            return product;
        }
        public async Task<ProductCategory> GetCategory(int id)
        {
           var category = await bridgeLabsShopDbContext.ProductCategories.SingleOrDefaultAsync(c=>c.Id == id);
            return category;
        }

       

   
    }
}
