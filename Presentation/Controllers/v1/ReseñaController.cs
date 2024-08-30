using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Servicio.Interface;

namespace Presentation.Controllers
{
    [ApiVersion("1.0")]
    public class ReseñaController : BaseApiController
    {
        private readonly IReseñaService _reseñaService;
        private readonly ILogger<ReseñaController> _logger;

        public ReseñaController(IReseñaService reseñaService, ILogger<ReseñaController> logger)
        {
            _reseñaService = reseñaService;
            _logger = logger;
        }
    }
}