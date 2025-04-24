using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TorneosITM.Services.Auth;

namespace TorneosITM.Services.Configuration
{
    public static class ServicesConfiguration
    {
        public static void Configuration(IServiceCollection services) { 
            services.AddScoped<ITorneoService, TorneoService>(); //CRUD Torneos
            services.AddScoped<IAuthService, AuthService>(); //Auth service
        }

        //Auth configuration
        public static void AuthConfiguration(IConfiguration configuration, IServiceCollection services) {
            string key = configuration.GetValue<string>("JwtConfiguration:Key")!;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            services.AddAuthentication(configuration =>
            {
                configuration.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configuration.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configuration => { 
                configuration.RequireHttpsMetadata = false;
                configuration.SaveToken = true;
                configuration.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization();
        }
    }
}
