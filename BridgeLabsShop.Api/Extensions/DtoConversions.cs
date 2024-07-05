using BridgeLabsShop.Api.Entities;
using BridgeLabsShop.Models.Dtos;

namespace BridgeLabsShop.Api.Extensions
{
    public static class DtoConversions
    {

        //this method will combine product and product categories and return an object of type ProductDto

        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products , IEnumerable<ProductCategory> productCategories) {
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id

                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = product.CategoryId,
                        CategoryName = productCategory.Name
                    }).ToList();
        
        
        }
    }
}
