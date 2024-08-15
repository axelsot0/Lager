using System.Text.Json.Serialization;

namespace Entidades.Dtos.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public string PhoneNumber { get; set; }
        public bool UserStatus { get; set; }
        public string Role { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

        public string? JWToken { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
    }
}
