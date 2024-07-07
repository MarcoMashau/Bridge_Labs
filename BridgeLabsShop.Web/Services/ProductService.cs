
using BridgeLabsShop.Models.Dtos;
using BridgeLabsShop.Web.Sercices.Contracts;
using System.Net.Http.Json;

namespace BridgeLabsShop.Web.Sercices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient? httpClient;
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;

        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                //we want our Jason to return an ienumerable type of ProductDto , and it should look into api/product to denote where it should look for the GetItems() in our web api to find the data.


                var response = await this.httpClient.GetAsync("api/Product");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message); ;
                }

            }

            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else { 
                var message = await response.Content.ReadAsStringAsync();
                    throw new Exception (message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
