using Microsoft.AspNetCore.Mvc.Filters;
// Permets de contrôler ce qui est passer en paramètre de notre requête,
// si les données sont celles attendues ou pas, avant d'exécuter la méthode (conforme au modèle attendu).
namespace Projet.API.REST.Swagger.Filters
{
    public class LogginActionFilter : IActionFilter
    {

        #region Injection de dépendance
        private readonly ILogger<LogginActionFilter> _logger;

        public LogginActionFilter(ILogger<LogginActionFilter> logger)
        {
            _logger = logger;
        }
        #endregion


        // Après l'action on exécute cette partie.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //log à la fin de l'action
            //throw new NotImplementedException();
            _logger.LogWarning($"FIN DE L'APPEL API : {context.ActionDescriptor.DisplayName} à {DateTime.Now}");
        }


        // Avant l'action on exécute cette partie.
        public void OnActionExecuting(ActionExecutingContext context)
        {

            //log le début de l'action
            _logger.LogWarning($"DEBUT DE L'APPEL API : {context.ActionDescriptor.DisplayName} à {DateTime.Now}");
        }
    }
}


#region Si je veux mettre mon filtre d'action comme attribut dans mon controller
//public class LogginActionFilter : ActionFilterAttribute, IActionFilter
//{

//    #region Injection de dépendance
//    private readonly ILogger<LogginActionFilter> _logger;

//    public LogginActionFilter(ILogger<LogginActionFilter> logger)
//    {
//        _logger = logger;
//    }
//    #endregion


//    // Après l'action on exécute cette partie.
//    public override void OnActionExecuted(ActionExecutedContext context)
//    {
//        //log à la fin de l'action
//        //throw new NotImplementedException();
//        _logger.LogWarning($"FIN DE L'APPEL API : {context.ActionDescriptor.DisplayName} à {DateTime.Now}");
//    }


//    // Avant l'action on exécute cette partie.
//    public override void OnActionExecuting(ActionExecutingContext context)
//    {

//        //log le début de l'action
//        _logger.LogWarning($"DEBUT DE L'APPEL API : {context.ActionDescriptor.DisplayName} à {DateTime.Now}");
//    }
//} 
#endregion
