using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.IdentityModel.Tokens;
using System.Reflection;
using System.Web.Http;

using WebApi.Models;
using WebApi.Models.Dto;

[assembly: OwinStartup("DeveloperConfiguration", typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
        private const string Issuer = "https://login.microsoftonline.com/cbbd5fc3-8924-44f4-aa61-e1683f47d182/v2.0";
        private const string Audience = "d8389fe2-002b-4d3d-bbe9-3475f8d5ab02";

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // This is the call to our swashbuckle config that needs to be called 
            SwaggerConfig.Register();

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseOAuthBearerAuthentication(
                new OAuthBearerAuthenticationOptions()
                {
                    AccessTokenFormat = new JwtFormat(
                            new TokenValidationParameters()
                            {
                                ValidAudiences = new[] { Audience, $"api://{Audience}" },
                                ValidateIssuer = false
                            },
                            new OpenIdConnectSecurityTokenProvider($"{Issuer}/.well-known/openid-configuration"))
                });
        }
    }
}
