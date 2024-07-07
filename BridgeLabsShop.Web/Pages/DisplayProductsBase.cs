using BridgeLabsShop.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace BridgeLabsShop.Web.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }

    }
}
