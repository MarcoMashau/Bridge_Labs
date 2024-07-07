using BridgeLabsShop.Models.Dtos;
using BridgeLabsShop.Web.Sercices.Contracts;
using BridgeLabsShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BridgeLabsShop.Web.Pages
{
    public class ProductDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public  IProductService ProductService {get;set;}


        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ProductDto Product { get;set;}

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync() {

            try
            {
                Product = await ProductService.GetItem(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                throw;
            }
        }

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);
               NavigationManager.NavigateTo("/ShoppingCart");

                //if (cartItemDto != null)
                //{
                //    ShoppingCartItems.Add(cartItemDto);
                //    await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
                //}


            }
            catch (Exception)
            {

                //Log Exception
            }
        }


    }
}
