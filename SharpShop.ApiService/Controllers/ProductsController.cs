using Microsoft.AspNetCore.Mvc;
using SharpShop.Business.Products;
using SharpShop.Models.Base;

namespace SharpShop.ApiService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET: api/products
        [HttpGet]
        public Task<IEnumerable<ProductModel>> GetAll(string sortOrder = "asc", string name = "")
        {
            return _productsService.GetAll(sortOrder, name);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public Task<ProductModel> Get([FromRoute] int id)
        {
            return _productsService.Get(id);
        }

        // POST: api/products
        [HttpPost, Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductModel>> Post([FromBody] ProductModel saveProduct)
        {
            var createdProduct = await _productsService.Save(saveProduct);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public Task<ProductModel> Put([FromRoute] int id, [FromBody] ProductModel updateProduct)
        {
            updateProduct.Id = id;
            return _productsService.Update(updateProduct);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public Task Delete([FromRoute] int id)
        {
            return _productsService.Delete(id);
        }
    }
}