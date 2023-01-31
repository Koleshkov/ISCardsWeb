using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using ISCardsWeb.Application.Common.Behaviors;
using ISCardsWeb.Application.Configurations;
using System.Reflection;
using ISCardsWeb.Aplication.Services.AccessTokenGenerators;
using ISCardsWeb.Aplication.Services.RefreshTokenGenerators;
using ISCardsWeb.Aplication.Services.RefreshTokenValidators;
using ISCardsWeb.Aplication.Services.TokenGenerators;
using ISCardsWeb.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ISCardsWeb.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblies(
                new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            MailKitConfiguration mailKitConfiguration = new();

            configuration.Bind("MailKitConfiguration", mailKitConfiguration);

            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    //get options from sercets.json
                    Server = mailKitConfiguration.Server,
                    Port = mailKitConfiguration.Port,
                    SenderName = mailKitConfiguration.SenderName,
                    SenderEmail = mailKitConfiguration.SenderEmail,

                    // can be optional with no authentication 
                    Account = mailKitConfiguration.SenderEmail,
                    Password = mailKitConfiguration.Password,
                    Security = true
                });
            });

            AuthenticationConfiguration authenticationConfiguration = new();
            configuration.Bind("Authentication", authenticationConfiguration);

            services.AddSingleton(authenticationConfiguration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecretKey)),
                    ValidIssuer = authenticationConfiguration.Issuer,
                    ValidAudience = authenticationConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();

            services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();

            services.AddSingleton<IRefreshTokenValidator, RefreshTokenValidator>();

            services.AddSingleton<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
