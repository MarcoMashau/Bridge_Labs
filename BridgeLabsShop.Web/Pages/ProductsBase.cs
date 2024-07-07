using BridgeLabsShop.Models.Dtos;
using BridgeLabsShop.Web.Sercices.Contracts;
//using BridgeLabsShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
namespace BridgeLabsShop.Web.Pages
{
    public class ProductsBase : ComponentBase 
    {

        //faxcrilitate dep injection 
        //we need to havea public  property of type IProductService , but this isnt any tupe of property, we are injecting it from 
        //IPropertyService 

        //WE MUST NOT FORGET TO register our ProductService class for dependency injection. Projet.cs

        [Inject]
        public IProductService productService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await productService.GetItems();
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos) {

            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;

        }

    }
}
