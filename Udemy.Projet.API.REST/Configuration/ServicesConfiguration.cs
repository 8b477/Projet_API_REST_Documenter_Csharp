namespace Udemy.Projet.API.REST.Configuration
{
    /// <summary>
    /// Toute nos extension de service ici pour plus de lisibilité du côté du program.cs
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
                    Title = "Mon super titre de merde",
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
            });

            return service;
        }

    }
}
