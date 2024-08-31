using Asp.Versioning;
using Entidades.Dtos.Entity;
using Entidades.Entity;
using Microsoft.AspNetCore.Mvc;
using Servicio.Interface;

namespace Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductosController : BaseApiController
    {

        private readonly IProductService _productService;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IProductService productService, ILogger<ProductosController> logger)
        {
            _productService = productService;
            _logger = logger;
        }



        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            _logger.LogInformation("Mostrando todas los productos");
            var productos = await _productService.GetAllProducts();
            if (productos == null)
            {
                _logger.LogInformation("Error extrayendo productos");
                return NotFound();
            }
            return Ok(productos);

        }


        //[HttpGet]
        //public async Task<IActionResult> GetProductsByUser(string IdUser)
        //{
        //    _logger.LogInformation("Mostrando todos los productos del usuario");
        //}


        [HttpPost("CrearProducto")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO newProducto)
        {
            _logger.LogInformation("Creando produto");
            var product = await _productService.CreateProduct(newProducto);
            return Ok(product);
        }



        [HttpPut("EditarProducto/{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] Producto updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest("Invalid data.");
            }

            bool result = await _productService.EditProduct(id, updatedProduct);
            if (result)
            {
                return Ok("Product updated successfully.");
            }
            else
            {
                return NotFound($"Product with id {id} not found.");
            }
        }




        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation($"API request received to delete product with id {id}");

            bool isDeleted = await _productService.DeleteProduct(id);
            if (isDeleted)
            {
                _logger.LogInformation($"Product with id {id} was successfully deleted.");
                return Ok($"Product with id {id} was successfully deleted.");
            }
            else
            {
                _logger.LogWarning($"Product with id {id} not found.");
                return NotFound($"Product with id {id} not found.");
            }
        }
    }
}
