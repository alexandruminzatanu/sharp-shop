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
        public string Get([FromQuery] string productId)
        {   
            return GetProduct.Get(productId);
        }

        [HttpPost(Name = "saveProduct")]
        public string Post([FromBody] Product saveProduct)
        {
            return SaveProduct.Save(saveProduct);
        }

        [HttpPut(Name = "updateProduct")]
        public string Put([FromBody] Product updateProduct)
        {
            return UpdateProduct.Update(updateProduct);
        }

        [HttpDelete(Name = "id")]
        public string Delete([FromQuery] string id)
        {
            return DeleteProduct.Delete(id);
        }
    }
}