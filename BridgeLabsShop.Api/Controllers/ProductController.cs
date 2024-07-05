using BridgeLabsShop.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BridgeLabsShop.Models.Dtos;
using BridgeLabsShop.Api.Extensions;


namespace BridgeLabsShop.Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository) { 
                this.productRepository = productRepository;
        }

        //an action method to get items and this gets its data from our Dto. 
        //but to obtain this, we need to createa project reference 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems() 
        {
            try
            {
                var products = await this.productRepository.GetItems();
                var productCategories = await this.productRepository.GetCategories();

                if (products == null || productCategories == null) { return NotFound(); }
                else
                {
                    //we need to join the collection of product categories and producs so we can return a productDto , which will inlcude a category name 
                    // we can create a Linq , we can create an extension method to return an object of type ProductDTo to our action method 


                    var productDto = products.ConvertToDto(productCategories);
                    return Ok(productDto);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong when making a connection to your Database");
            }
        }

    }
}

//code to ensure an object of ProductRepository is automatically injected  in our our ProductController constructor
//via dep injection 