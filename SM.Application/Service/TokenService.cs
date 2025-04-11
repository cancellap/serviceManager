using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SM.Application.DTOs;
using SM.Application.Interfaces;
using SM.Domaiin.Entities;
using SM.Domaiin.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SM.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly IUsuarioRepository _usuarioRepository;

        public TokenService(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<string> GenerateToken(LoginDto user)
        {
            Usuario userDataBase = await _usuarioRepository.GetByUsername(user.Username);
            if (userDataBase == null || !BCrypt.Net.BCrypt.Verify(user.Password, userDataBase.Password))
            {
                return string.Empty;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Username", user.Username),
                new Claim("Id", userDataBase.Id.ToString())
            };



            foreach (UsuarioRole userRole in userDataBase.UsuarioRoles)
            {
                var roleName = userRole.Role?.Nome;
                if (!string.IsNullOrEmpty(roleName))
                {
                    Claim clain1 = (new Claim("RoleId", userRole.RoleId.ToString()));
                    Claim clain2 = (new Claim("Role", roleName));

                    claims.AddRange(new[] { clain1, clain2 });
                }
            }

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        public int GetIdToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;


                if (int.TryParse(userId, out int id))
                {
                    return id;
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao extrair ID do token: {ex.Message}");
                return 0;
            }
        }

    }
}
