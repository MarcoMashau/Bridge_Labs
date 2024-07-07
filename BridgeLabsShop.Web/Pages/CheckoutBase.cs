
using BridgeLabsShop.Models.Dtos;
using BridgeLabsShop.Web.Services.Contracts;
using BridgeLabsShop.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace BridgeLabsShop.Web.Pages
{
    public class CheckoutBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        protected IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        protected int TotalQty { get; set; }

        protected string PaymentDescription { get; set; }

        protected decimal PaymentAmount { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

       // [Inject]
       // public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }


        protected string DisplayButtons { get; set; } = "block";

        private async Task ReloadAfterDelay(int milliseconds)
        {
            await Task.Delay(milliseconds);
            StateHasChanged();
        }



        protected override async Task OnInitializedAsync()
        {
            try
            {
            
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);

                if (ShoppingCartItems != null && ShoppingCartItems.Count() > 0)
                {
                    Guid orderGuid = Guid.NewGuid();

                    PaymentAmount = ShoppingCartItems.Sum(p => p.TotalPrice);
                    TotalQty = ShoppingCartItems.Sum(p => p.Qty);
                    PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}";

                }
                else
                {
                
                    DisplayButtons = "none";
                }

            }
            catch (Exception)
            {
  
                //Log exception
                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                   await Js.InvokeVoidAsync("initPayPalButton");
                }
                else {

                    //await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {
                await Js.InvokeVoidAsync("initPayPalButton");

                //throw;
            }
            
        }


    }
}