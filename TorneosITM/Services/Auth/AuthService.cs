using TorneosITM.Data.DTOs;
using TorneosITM.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TorneosITM.Data;

namespace TorneosITM.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public AuthService(AppDbContext context, IConfiguration configuration) {
            this.context = context;
            this.configuration = configuration;
        }

        private string GenerateToken(int idUsuario) {
            string key = configuration.GetValue<string>("JwtConfiguration:Key")!;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString(), ClaimValueTypes.Integer32));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(keyBytes);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor {
                Subject = claims, Expires = DateTime.UtcNow.AddMinutes(5), SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);
            string generatedJwt = tokenHandler.WriteToken(token);

            return generatedJwt;
        }
        public async Task<AuthResponse?> GetToken(AuthRequest authRequest)
        {
            AdministradorItm? foundedUser = context.AdministradoresItm.FirstOrDefault(u => u.Usuario == authRequest.Usuario && u.Clave == authRequest.Clave);
            if (foundedUser == null) { AuthResponse? response = null; return response; }
            else
            {
                string generatedJwt = GenerateToken(foundedUser.IdAministradorItm);
                AuthResponse response = new AuthResponse { 
                    Token = generatedJwt,Resultado = true, Mensaje = "Credenciales válidas. Autenticado."
                };
                return response;
            }
        }


    }

    public interface IAuthService
    {
        Task<AuthResponse> GetToken(AuthRequest authRequest);
    }
}
