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

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

       

   
    }
}
