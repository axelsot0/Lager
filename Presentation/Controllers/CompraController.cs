using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio.Interface;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _compraService;
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(ICompraService compraService, ILogger<ComprasController> logger)
        {
            _compraService = compraService;
            _logger = logger;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetAllCompras()
        {
            try
            {
                var compras = await _compraService.GetAllCompras();
                return Ok(compras);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all purchases: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        
        [HttpGet("{idUser}")]
        public async Task<ActionResult<IEnumerable<Compra>>> GetComprasByUser(int idUser)
        {
            try
            {
                var compras = await _compraService.GetComprasByUser(idUser);
                if (!compras.Any())
                {
                    return NotFound($"No purchases found for user ID {idUser}");
                }
                return Ok(compras);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving purchases for user {idUser}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<Compra>> CreateCompra([FromBody] Compra newCompra)
        {
            if (newCompra == null)
            {
                return BadRequest("Invalid purchase data");
            }

            try
            {
                var createdCompra = await _compraService.CreateCompra(newCompra);
                return CreatedAtAction(nameof(GetComprasByUser), new { idUser = createdCompra.IdUsuario }, createdCompra);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating purchase: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            try
            {
                var result = await _compraService.DeleteCompra(id);
                if (!result)
                {
                    return NotFound($"Compra with id {id} not found");
                }
                return Ok($"Compra with id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting compra with id {id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
