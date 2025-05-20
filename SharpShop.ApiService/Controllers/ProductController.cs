using Microsoft.AspNetCore.Mvc;
using SharpShop.Business;
using SharpShop.Models.Base;

namespace SharpShop.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {


        [HttpGet(Name = "productId")]
        public Task<Product> Get([FromQuery] int productId)
        {   
            return GetProduct.Get(productId);
        }

        [HttpPost(Name = "saveProduct")]
        public Task<Product> Post([FromBody] Product saveProduct)
        {
            return SaveProduct.Save(saveProduct);
        }

        [HttpPut(Name = "updateProduct")]
        public Task<Product> Put([FromBody] Product updateProduct)
        {
            return UpdateProduct.Update(updateProduct);
        }

        [HttpDelete(Name = "id")]
        public Task Delete([FromQuery] int id)
        {
            return DeleteProduct.Delete(id);
        }
    }
}