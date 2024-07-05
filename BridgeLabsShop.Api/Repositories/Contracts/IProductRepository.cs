using BridgeLabsShop.Api.Entities;

namespace BridgeLabsShop.Api.Repositories.Contracts
{
    public interface IProductRepository
    {

        //my method definitions 
        //the below is a method that will return Generic Type Objects , 
        //i want my first method to get all items so it will return an IEnumerable collection  of type Product 
        //so youll see that as a type, ive used Product (an object) , this allows a method that impliments 
        //this to run asyncrounsly 
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();

        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);

 
    }
}
