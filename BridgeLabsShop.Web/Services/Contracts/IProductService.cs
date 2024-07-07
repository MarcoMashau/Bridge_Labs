namespace BridgeLabsShop.Web.Sercices.Contracts;
using BridgeLabsShop.Models;
using BridgeLabsShop.Models.Dtos;

public interface IProductService
{

    Task<IEnumerable<ProductDto>> GetItems();
    Task<ProductDto> GetItem(int id);

}
