using BridgeLabsShop.Models.Dtos;
using BridgeLabsShop.Web.Sercices.Contracts;
using BridgeLabsShop.Web.Services;
using BridgeLabsShop.Web.Services.Contracts;
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
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

       // [Inject]
        //public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        [Inject]
       // public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();
                var shoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                var totalQty = shoppingCartItems.Sum(i => i.Qty);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
           
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
