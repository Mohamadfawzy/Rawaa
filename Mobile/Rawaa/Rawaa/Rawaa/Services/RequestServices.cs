using Rawaa.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rawaa.Services
{
    public static class RequestServices
    {
        public static async Task<bool> PostProductToCart(Product product)
        {
            var provider = new RequestProvider<Cart>();
            var cart = new Cart()
            {
                CustomerId = Convert.ToInt32(AppSettings.UserId),
                ProductId = product.Id,
                Quantity = 1,
                Taste = 0,
                Size = 1,
                DrinkId =1,
            };
            //var s = "{\"CustomerId\":4,\"ProductId\":2,\"Quantity\":1,\"Taste\":1,\"Size\":1,\"DrinkId\":1}";

            var rs = await provider.PostOneAsync<Cart>(cart, $"{AppSettings.currentLang}/api/client/cart");
            if (rs == null) 
                return false;
            return true;
        }
    }
}
