using Microsoft.AspNetCore.Mvc;
using SharpShop.Business.Products;
using SharpShop.Models.Base;
using Microsoft.EntityFrameworkCore;

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
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAll([FromQuery] string sortOrder = "asc", [FromQuery] string name = "")
        {
            try
            {
                sortOrder = (sortOrder ?? "asc").Trim().ToLowerInvariant();
                if (sortOrder != "asc" && sortOrder != "desc")
                    return BadRequest("sortOrder must be 'asc' or 'desc'.");

                var products = await _productsService.GetAll(sortOrder, name ?? string.Empty);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return ToProblem(ex);
            }
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductModel>> Get([FromRoute] int id)
        {
            try
            {
                var product = await _productsService.Get(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return ToProblem(ex);
            }
        }

        // POST: api/products
        [HttpPost, Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductModel>> Post([FromBody] ProductModel saveProduct)
        {
            try
            {
                var createdProduct = await _productsService.Save(saveProduct);
                return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return ToProblem(ex);
            }
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ProductModel>> Put([FromRoute] int id, [FromBody] ProductModel updateProduct)
        {
            try
            {
                updateProduct.Id = id;
                var updated = await _productsService.Update(updateProduct);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return ToProblem(ex);
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _productsService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ToProblem(ex);
            }
        }

        // Maps common exceptions to RESTful ProblemDetails responses
        private ActionResult ToProblem(Exception ex)
        {
            return ex switch
            {
                ArgumentNullException or ArgumentException => BadRequest(new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest
                }),
                KeyNotFoundException => NotFound(new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = ex.Message,
                    Status = StatusCodes.Status404NotFound
                }),
                DbUpdateConcurrencyException => Conflict(new ProblemDetails
                {
                    Title = "Conflict",
                    Detail = "A concurrency conflict occurred while processing the request.",
                    Status = StatusCodes.Status409Conflict
                }),
                DbUpdateException => UnprocessableEntity(new ProblemDetails
                {
                    Title = "Unprocessable Entity",
                    Detail = "The request could not be processed due to a data update error.",
                    Status = StatusCodes.Status422UnprocessableEntity
                }),
                _ => Problem(
                    title: "Internal Server Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError)
            };
        }
    }
}