using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;


namespace Simple.API.Configs
{
    /// <summary>
    /// Configure JWT settings
    /// </summary>
    public static class JwtConfigService
    {
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration config)
        {
            var issuer = config["Jwt:Issuer"];
            var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    // Clock skew compensates for server time drift.
                    // We recommend 5 minutes or less:
                    ClockSkew = TimeSpan.FromMinutes(0), // Set to 5 or 10 mins when implementing refreshing of token

                    // Ensure the token hasn't expired:
                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    // Ensure the token audience matches our audience value (default true):
                    ValidateAudience = true,
                    ValidAudience = issuer,

                    // Ensure the token was issued by a trusted authorization server (default true):
                    ValidateIssuer = true,
                    ValidIssuer = issuer
                };
            });

            return services;
        }
    }
}
