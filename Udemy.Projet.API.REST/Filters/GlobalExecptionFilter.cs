using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Projet.API.REST.Swagger.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {

        #region Injection de dépendance
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        } 
        #endregion

        // Se déclenche pour chaque exceptions levée par les controllers, non géré par notre application.
        // En gros les erreurs 500 ou autres erreur que l'on aurait oublié de gérer.
        public void OnException(ExceptionContext context)
        {

            _logger.LogError(context.Exception.Message);

            context.Result = new ContentResult
            {
                Content = $"Exeption depuis ma classe GlobalExceptionFIlter {context.Exception.ToString()}"
            };
        }
    }
}
