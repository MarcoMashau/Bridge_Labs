
using BridgeLabsShop.Api.Data;
using BridgeLabsShop.Api.Entities;
using BridgeLabsShop.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using BridgeLabsShop.Api.Repositories.Contracts;


namespace BridgeLabsShop.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly BridgeLabsShopDbContext bridgeLabsShopDbContext;

        public ShoppingCartRepository(BridgeLabsShopDbContext bridgeLabsShopDbContext)
        {
            this.bridgeLabsShopDbContext = bridgeLabsShopDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.bridgeLabsShopDbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in this.bridgeLabsShopDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    //if the iteam does indeed exist for addition, add it. 
                    var result = await this.bridgeLabsShopDbContext.CartItems.AddAsync(item);
                    await this.bridgeLabsShopDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;

        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await this.bridgeLabsShopDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                this.bridgeLabsShopDbContext.CartItems.Remove(item);
                await this.bridgeLabsShopDbContext.SaveChangesAsync();
            }

            return item;

        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.bridgeLabsShopDbContext.Carts
                          join cartItem in this.bridgeLabsShopDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in this.bridgeLabsShopDbContext.Carts
                          join cartItem in this.bridgeLabsShopDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await this.bridgeLabsShopDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await this.bridgeLabsShopDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }
    }
}