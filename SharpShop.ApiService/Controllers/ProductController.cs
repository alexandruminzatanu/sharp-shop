using Microsoft.AspNetCore.Mvc;
using SharpShop.Business;

namespace SharpShop.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {


        [HttpGet(Name = "product")]
        public string Get([FromQuery] string name)
        {   
            return GetProduct.Get(name);
        }

        [HttpPost(Name = "product")]
        public string Post([FromBody] string name)
        {
            return SaveProduct.Save(name);
        }
    }
}