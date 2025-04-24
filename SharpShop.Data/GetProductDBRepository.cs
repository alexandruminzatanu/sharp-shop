using SharpShop.Models.Base;
using System.Text.Json;

namespace SharpShop.Data
{
   public class GetProductDB
    {
       async public static Task<string> Get(string productId)
        {
            using (var context = new SharpShopContext())
            {
                
                var product = await context.Products.FindAsync(productId);
                if (product != null)
                {
                    string jsonString = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });
                    return jsonString;

                }
                else
                {
                    return "";
                }
            }


        }

    }
}
