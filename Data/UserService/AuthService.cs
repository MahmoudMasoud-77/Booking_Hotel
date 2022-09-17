using Booking_Hotel.DTO;
using Booking_Hotel.Data._JWT;
using Booking_Hotel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking_Hotel.Data.UserService
{
    public class AuthService:IAuthService
    {
        private readonly UserManager<Guest> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT jwt;
        public AuthService( UserManager<Guest> _userManager,RoleManager<IdentityRole> _roleManager,
                            IOptions<JWT> _jwt)
        {
            this.userManager = _userManager;
            this.roleManager = _roleManager;
            this.jwt = _jwt.Value;
        }
        public async Task<AuthDto> Register(RegisterDto UserDto)
        {
            if (await userManager.FindByEmailAsync(UserDto.Email) is not null)
            {
                return new AuthDto { Message = "Email is already exist" };
            }
            if (await userManager.FindByNameAsync(UserDto.UserName) is not null)
            {
                return new AuthDto { Message = "User Name is already exist" };
            }
            Guest user = new Guest
            {
                UserName = UserDto.UserName,
                Email = UserDto.Email,
                PasswordHash = UserDto.Password,
            };

            //Hashing by Password
            IdentityResult result = await userManager.CreateAsync(user, UserDto.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthDto { Message = errors };
            }
            //Create Token
            var jwtSecurityToken = await CreateJwtToken(user);
            //cluims
            return new AuthDto
            {
                Id = user.Id,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };

        }
        public async Task<AuthDto> Login(TokenRequestDto UserDto)
        {
            //check -Create Token
            AuthDto authDto = new AuthDto();

            Guest user = await userManager.FindByEmailAsync(UserDto.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, UserDto.Password))
            {
                authDto.Message = "Email or Password is incorrect!";
                return authDto;
            }

            //Create Token
            var jwtSecurityToken = await CreateJwtToken(user);
            //Get Roles
            var rolesList = await userManager.GetRolesAsync(user);
            authDto.Id = user.Id;
            authDto.IsAuthenticated = true;
            authDto.IsSuccess = true;
            authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authDto.ExpiresOn = jwtSecurityToken.ValidTo;
            authDto.Roles = rolesList.ToList();

            return authDto;
        }


        public async Task<string> AddRole(AddRoleDto model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user is null || !await roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            IdentityResult result = await userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }
        private async Task<JwtSecurityToken> CreateJwtToken(Guest user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.ValidIssuer,
                audience: jwt.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<Guest> GetGuestInfo(string userName)
        {
            return (await userManager.FindByNameAsync(userName));
        }
    }
}
