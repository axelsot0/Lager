using Entidades;
using Entidades.Dtos.Account;
using Entidades.Dtos.Email;
using Entidades.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Servicio.Enums;
using Servicio.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Servicio.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             IEmailService emailService,
                             IOptions<JWTSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }



        #region PublicMethods

        //USER AUTHENTICATION
        public async Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered under Email {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credential for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed for {request.Email}";
                return response;
            }
            if (user.IsActive == false)
            {
                response.HasError = true;
                response.Error = $"Your account user {request.Email} is not active please get in contact with a manager";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Role = roleList.FirstOrDefault();

            response.IsVerified = user.EmailConfirmed;
            response.LastName = user.LastName;
            response.UserStatus = true;

            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }


        //SINGOUT
        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //REGISTER USER

        public async Task<Entidades.Wrappers.Response<int>> RegisterUserAsync(RegisterRequest request, string origin, string UserRoles)
        {
            Entidades.Wrappers.Response<int> response = new();
            response.Succeeded = true;

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.Succeeded = false;
                response.Message = $"Username {request.UserName} is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.Succeeded = false;
                response.Message = $"Email {request.Email} is already registered";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                LastName = request.LastName,
                UserName = request.UserName,
                IsActive = false,
                PhoneNumber = request.PhoneNumber
            };

            if (UserRoles == RolesEnum.Cliente.ToString() || UserRoles == RolesEnum.Tienda.ToString() || UserRoles == RolesEnum.SuperAdmin.ToString())
            {
                user.IsActive = true;
            }

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles);

                var verificationURI = await SendVerificationUri(user, origin);

                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <style>\r\n        body {{\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f4f4f4;\r\n            margin: 0;\r\n            padding: 0;\r\n            color: #333;\r\n        }}\r\n        .container {{\r\n            width: 100%;\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            background-color: #ffffff;\r\n            border-radius: 10px;\r\n            overflow: hidden;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }}\r\n        .header {{\r\n            background-color: #007BFF;\r\n            color: #ffffff;\r\n            padding: 20px;\r\n            text-align: center;\r\n        }}\r\n        .header h1 {{\r\n            margin: 0;\r\n            font-size: 24px;\r\n        }}\r\n        .body {{\r\n            padding: 20px;\r\n        }}\r\n        .body p {{\r\n            font-size: 16px;\r\n            line-height: 1.5;\r\n        }}\r\n        .cta-button {{\r\n            display: block;\r\n            width: 200px;\r\n            margin: 20px auto;\r\n            padding: 15px;\r\n            background-color: #007BFF;\r\n            color: #ffffff;\r\n            text-align: center;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n            font-size: 16px;\r\n        }}\r\n        .cta-button:hover {{\r\n            background-color: #0056b3;\r\n        }}\r\n        .footer {{\r\n            background-color: #f4f4f4;\r\n            padding: 10px;\r\n            text-align: center;\r\n            font-size: 12px;\r\n            color: #777;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <div class=\"header\">\r\n            <h1>Confirm Your Registration</h1>\r\n        </div>\r\n        <div class=\"body\">\r\n            <p>Hello,</p>\r\n            <p>Thank you for registering with us. To complete your registration, please click the button below to verify your email address:</p>\r\n            <a href=\"{verificationURI}\" class=\"cta-button\">Confirm Registration</a>\r\n            <p>If you did not register for this account, please disregard this email.</p>\r\n        </div>\r\n        <div class=\"footer\">\r\n            <p>&copy; 2024 Axel & Yahinniel. All rights reserved.</p>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.Succeeded = false;
                response.Message = $"An error occurred trying to register the user.";
                return response;
            }

            response.Message = "Favor confirmar la cuenta.";
            return response;
        }


        //RESETPASSWORD
        public async Task<Entidades.Wrappers.Response<int>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            Entidades.Wrappers.Response<int> response = new();
            response.Succeeded = true;
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.Succeeded = false;
                response.Message = $"An error occurred while reset password";
                return response;
            }

            return response;
        }


        //GETBYID
        public async Task<DtoAccounts> GetByIdAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            DtoAccounts dtoaccount = new()
            {
                Email = user.Email,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName,
                IsActive = user.IsActive,
                PhoneNumber = user.PhoneNumber,
                Password = user.PasswordHash
            };
            return dtoaccount;
        }



        //CONFIRMACCOUNT
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No user register under this {user.Email} account";
            }

            token = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirm for {user.Email} you can now  use the app";
            }
            else
            {
                return $"An error occurred wgile confirming {user.Email}.";
            }
        }

        //FORGOTPASSWORD
        public async Task<Entidades.Wrappers.Response<int>> ForgotPassswordAsync(ForgotPasswordRequest request, string origin)
        {
            Entidades.Wrappers.Response<int> response = new();
            response.Succeeded = true;
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationURI = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationURI}",
                Subject = "reset password"
            });

            return response;

        }

        public async Task<DtoAccounts> GetByEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            DtoAccounts dtoaccount = new()
            {
                Email = user.Email,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName,
                IsActive = user.IsActive,
                PhoneNumber = user.PhoneNumber,
            };
            return dtoaccount;
        }


        //DELETE USER
        public async Task Remove(DtoAccounts account)
        {
            Entidades.Wrappers.Response<int> response = new();

            var user = await _userManager.FindByIdAsync(account.Id);
            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"This user does not exist now";
            }
            await _userManager.DeleteAsync(user);
        }

        //USERS GETALL

        public async Task<List<DtoAccounts>> GetAllTiendas()
        {
            var userList = await _userManager.Users.OrderBy(u => u.LastName).ToListAsync();
            List<DtoAccounts> DtoUserList = new();

            foreach (var user in userList)
            {
                var userDto = new DtoAccounts();
                userDto.LastName = user.LastName;
                userDto.UserName = user.UserName;
                userDto.IsActive = user.IsActive;
                userDto.Email = user.Email;
                userDto.Id = user.Id;

                // esto no esta muy bien implemntado, se necesita elegir el role del usuario pero el metodo GetRolesAsync 
                // solo funciona con listas de string y el usuario solamente puede tener un role.

                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                userDto.Role = roles.FirstOrDefault();


                if (userDto.Role == RolesEnum.Tienda.ToString())
                {
                    DtoUserList.Add(userDto);
                }
            }

            return DtoUserList;
        }


        public async Task<DtoAccounts> FindUserWithFilters(Entidades.Filtro.FilterFindUser user)
        {
            var applicationUser = await _userManager.FindByNameAsync(user.NameTienda);
            var userDto = new DtoAccounts();

            if (string.Equals(applicationUser.LastName, user.NameTienda, StringComparison.OrdinalIgnoreCase) && user != null)
            {
                userDto.LastName = applicationUser.LastName;
                userDto.IsActive = applicationUser.IsActive;
                userDto.Email = applicationUser.Email;
                userDto.Id = applicationUser.Id;

                if (userDto.IsActive == false)
                {
                    return null;
                }
                else
                {
                    return userDto;
                }
            }
            else
            {
                return null;
            }
        }

        //CHANGE USER STATUS
        public async Task<Entidades.Wrappers.Response<int>> ChangeUserStatus(ChangeStatusUser request)
        {
            Entidades.Wrappers.Response<int> response = new();
            response.Succeeded = true;

            var userget = await _userManager.FindByIdAsync(request.IdUser);
            {
                userget.IsActive = request.IsActive;
            }

            var result = await _userManager.UpdateAsync(userget);

            if (!result.Succeeded)
            {
                response.Succeeded = false;
                response.Message = $"There was an error while trying to update the user{userget.UserName}";
            }
            return response;
        }

        //EDITUSER
        public async Task<Entidades.Wrappers.Response<int>> UpdateUserAsync(RegisterRequest request)
        {
            Entidades.Wrappers.Response<int> response = new();
            response.Succeeded = true;

            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"User with ID {request.Id} not found.";
                return response;
            }


            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IsActive = request.IsActive;

            if (!string.IsNullOrEmpty(request.ConfirmPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
                if (!result.Succeeded)
                {
                    response.Succeeded = false;
                    response.Message = $"Failed to reset password for user {user.UserName}.";
                    return response;
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                response.Succeeded = false;
                response.Message = $"Failed to update user {user.UserName}.";
            }

            return response;
        }
        #endregion


        #region PrivateMethods
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }


        private async Task<string> SendVerificationUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/Account/confirm-email";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }


        #endregion
    }
}
