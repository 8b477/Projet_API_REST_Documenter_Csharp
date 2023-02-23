using System.Reflection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

using Projet.API.REST.Swagger.Filters;

namespace Udemy.Projet.API.REST.Configuration
{
    /// <summary>
    /// Toute nos extension de service ici pour plus de lisibilité côté du program.cs
    /// *(* NE PAS OUBLIER DE RENDRE LA CLASSE STATIC *)*
    /// </summary>

    public static class ServicesConfiguration
    {
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
                        Url = new Uri("https://www.google.be")
                    }
                });
                //Afficher la documentation => ///<summary> directement dans la page visuel de swagger
                //lecture du fichier XML pour swagger
                var xmlHelp = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //Ci dessus :
                //=> On récupère ici le nom du fichier générée à l'assemblage du projet et on lui rajoute l'extension .xml

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlHelp));
            });

            return service;
        }

        public static IServiceCollection AddAuthentificationService(this IServiceCollection service)
        {

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.Authority = "https://dev-qq7s2j4r0zzrukm8.us.auth0.com";
                options.Audience = "https://testAuth";
            });

            return service;
        }

        public static IServiceCollection AddControllerService(this IServiceCollection service)
        {
#region Ici on ajoute à chaque controllers l'authentification.

            service.AddControllers(options =>
    {
        var policies = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();

        options.Filters.Add(new AuthorizeFilter(policies));

#endregion

#region Ici on ajoute nos filtre perso sur chaques controllers.

        // Impossible d'ajouter mon LogginActionFilter comme ceci pour le propager dans tout mon controller
        // car il manque notre dépendance => logger !
        //options.Filters.Add(new LogginActionFilter());

        // Faire comme ceci quand ont à une dépendance dans notre classe.
        options.Filters.Add<LogginActionFilter>();  

        options.Filters.Add<GlobalExceptionFilter>();  

        options.Filters.Add(new FormattingResultFilter());

#endregion

    });

            return service;
        }
    }
}
