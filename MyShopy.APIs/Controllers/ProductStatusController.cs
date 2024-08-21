using Microsoft.AspNetCore.Mvc;
using MyShopy.Models.Models;
using MyShopy.Models.Services;

namespace MyShopy.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductStatusController : ControllerBase
    {
        private readonly ProductStatusService _productStatusService;
        private readonly ILogger<ProductStatusController> _logger;

        public ProductStatusController(ProductStatusService productStatusService, ILogger<ProductStatusController> logger)
        {
            _productStatusService = productStatusService;
            _logger = logger;
        }

        // GET: api/ProductStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> GetProductStatuses()
        {
            try
            {
                var productStatuses = await _productStatusService.GetProductStatusesAsync();
                return Ok(productStatuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product statuses");
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/ProductStatus/ProductStatusById
        [HttpGet("ProductStatusById")]
        public async Task<ActionResult<ProductStatus>> GetProductStatusById([FromQuery] int id)
        {
            try
            {
                var productStatus = await _productStatusService.GetProductStatusAsync(id);

                if (productStatus == null)
                {
                    return NotFound();
                }

                return Ok(productStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product status by ID {id}");
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/ProductStatus
        [HttpPut]
        public async Task<IActionResult> PutProductStatus([FromQuery] int id, [FromBody] ProductStatus productStatus)
        {
            if (id != productStatus.StatusId)
            {
                return BadRequest("Product ID does not match");
            }

            try
            {
                await _productStatusService.UpdateProductStatusAsync(productStatus);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product status with ID {id}");
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/ProductStatus
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> PostProductStatus([FromBody] ProductStatus productStatus)
        {
            try
            {
                await _productStatusService.CreateProductStatusAsync(productStatus);
                return CreatedAtAction(nameof(GetProductStatusById), new { id = productStatus.StatusId }, productStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product status");
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/ProductStatus
        [HttpDelete]
        public async Task<IActionResult> DeleteProductStatus([FromQuery] int id)
        {
            try
            {
                await _productStatusService.DeleteProductStatusAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product status with ID {id}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
