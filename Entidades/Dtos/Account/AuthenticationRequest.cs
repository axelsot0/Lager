using Swashbuckle.AspNetCore.Annotations;

namespace Entidades.Dtos.Account
{
    public class AuthenticationRequest
    {
        [SwaggerParameter(Description = "Correo del usuario que desea iniciar seccion")]
        public string Email { get; set; }
        [SwaggerParameter(Description = "Contrasenia del usuario que desea iniciar seccion")]
        public string Password { get; set; }
    }
}
