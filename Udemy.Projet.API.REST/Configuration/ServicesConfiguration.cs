using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;

using Projet.API.REST.Swagger.Filters;
using Projet.API.REST.Swagger.Token;

namespace Udemy.Projet.API.REST.Configuration
{
    /// <summary>
    /// Toute nos extension de service ici pour plus de lisibilité côté du program.cs
    /// *(* NE PAS OUBLIER DE RENDRE LA CLASSE STATIC *)*
    /// </summary>

    public static class ServicesConfiguration
    {
        /// <summary>
        /// Extension => Configuration Swagger / En tête visuel / XML Doc.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenService(this IServiceCollection service)
        {

            service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",// => crée un bug swagger ne trouve plus la page !!
                    Title = "Organise ce que tu veux faire !",
                    Description = "API pour faciliter sa gestion de l'emploie du temp, via une TODO liste.",

                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "8b477",
                        Email = "jonathan_buchet@outlook.fr"
                    },

                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Nom de la license",
                        Url = new Uri("https://www.google.be") // URI bidon pour le test.
                    }
                });
                //Afficher la documentation => /// <summary> directement dans la page visuel de swagger
                //lecture du fichier XML pour swagger
                var xmlHelp = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //Ci dessus :
                //=> On récupère ici le nom du fichier générée à l'assemblage du projet et on lui rajoute l'extension .xml

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlHelp));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Utiliser 'Authorization : Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type =ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                 new string []{}
                }
                });
            });
        return service;
        }
        /// <summary>
        /// Extension => Configuration JwtBearer
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthentificationService(this IServiceCollection service)
        {

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    IssuerSigningKey = TokenHelper.SIGNING_KEY,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
            #region Authentification via Aut0.com
            //    .AddJwtBearer(options =>
            //{
            //    options.Authority = "https://dev-qq7s2j4r0zzrukm8.us.auth0.com";
            //    options.Audience = "https://testAuth";
            //}); 
            #endregion

         return service;
        }
        /// <summary>
        /// Extension controllers sur chacun d'entre eux :
        /// => Ajout de l'authentification sur chacun.
        /// => Ajout des filtres.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddControllerService(this IServiceCollection service)
        {
            //L'authentification.
            service.AddControllers(options =>
            {
                var policies = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();

                options.Filters.Add(new AuthorizeFilter(policies));

                // Ajout de nos filtre.
                // -------------------
                // ** Impossible d'ajouter mon LogginActionFilter comme ceci pour le propager dans tout mon controller **
                // ** car il manque notre dépendance => logger ! **

                // => options.Filters.Add(new LogginActionFilter());

                // Faire comme ceci quand ont à une dépendance dans notre classe.
                options.Filters.Add<LogginActionFilter>();  

                options.Filters.Add<GlobalExceptionFilter>();  

                options.Filters.Add(new FormattingResultFilter());
            });
        return service;
        }
    }
}
