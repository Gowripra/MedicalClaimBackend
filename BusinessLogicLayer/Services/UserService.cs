using DataAccesLayer.Models;
using Medical_Claim.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService:IUserService
    {
        private readonly DataContext _context;
        private string secretKey;
        //private readonly IConfiguration _configuration;

        public UserService(DataContext context,IConfiguration configuration)
        {
            _context = context;
            //_configuration = configuration;
            secretKey = configuration.GetSection("ApiSettings:Secret").Value;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.Email == loginRequest.Email );
            //if (user!=null)
            //{
            //    LoginResponseDTO response = new()
            //    {
            //        Email = user.Email,
            //        Role=user.Role,
            //        Token="",
            //    };
            //    return response;
            //}
            //return new LoginResponseDTO();
            if (user != null && VerifyPassword(loginRequest.Password,user.PasswordHash,user.PasswordSalt))
            {
                //if user was found generate a token
                var tokenHandler = new JwtSecurityTokenHandler();
                //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                //    _configuration.GetSection("ApiSettings:Secret").Value));
                var key = Encoding.ASCII.GetBytes(secretKey);
                //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new System.Security.Claims.Claim[]
                    {
                    new System.Security.Claims.Claim(ClaimTypes.Name, user.Email.ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
                {
                    Token = tokenHandler.WriteToken(token),
                    Role = user.Role,
                    Email = user.Email
                };

                return loginResponseDTO;
            }
           

            return new LoginResponseDTO()
            {
                Token = "",
                Role = "",
                Email = ""
            };
        }
        private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
                for (int i = 0; i < computedHash.Length; i++)
                { // Loop through the byte array
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true; //if no mismatches.
        }
    }
}
