using Microsoft.AspNetCore.Mvc;
using SharpShop.Business.Product;
using SharpShop.Models.Base;

namespace SharpShop.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {

            _productService = productService;
        }

        [HttpGet(Name = "productId")]
        public Task<ProductModel> Get([FromQuery] int productId)
        {   
            return _productService.Get(productId);
        }

        [HttpPost(Name = "saveProduct")]
        public Task<ProductModel> Post([FromBody] ProductModel saveProduct)
        {
            return _productService.Save(saveProduct);
        }

        [HttpPut(Name = "updateProduct")]
        public Task<ProductModel> Put([FromBody] ProductModel updateProduct)
        {
            return _productService.Update(updateProduct);
        }

        [HttpDelete(Name = "id")]
        public Task Delete([FromQuery] int id)
        {
            return _productService.Delete(id);
        }
    }
}