using Entidades;
using Entidades.Dtos.Account;
using Entidades.Wrappers;

namespace Servicio.Interface
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request);
        Task<Response<int>> ChangeUserStatus(RegisterRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<DtoAccounts> FindUserWithFilters(FilterFindUser user);
        Task<Response<int>> ForgotPassswordAsync(ForgotPasswordRequest request, string origin);
        Task<List<DtoAccounts>> GetAllUsers();
        Task<DtoAccounts> GetByEmail(string Email);
        Task<DtoAccounts> GetByIdAsync(string UserId);
        Task<Response<int>> RegisterUserAsync(RegisterRequest request, string origin, string UserRoles);
        Task Remove(DtoAccounts account);
        Task<Response<int>> ResetPasswordAsync(ResetPasswordRequest request);
        Task SingOutAsync();
        Task<Response<int>> UpdateUserAsync(RegisterRequest request);
    }
}