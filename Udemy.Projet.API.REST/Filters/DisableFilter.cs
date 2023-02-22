//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace Projet.API.REST.Swagger.Filters
//{
//    public class DisableFilter : Attribute, IResourceFilter
//    {
//        Après l'action on exécute cette partie.
//        public void OnResourceExecuted(ResourceExecutedContext context)
//        {
//            // throw new NotImplementedException();
//        }
//        Avant l'action on exécute cette partie.
//        public void OnResourceExecuting(ResourceExecutingContext context)
//        {
//            // Si la route contient un certain chemin alors on avertit l'utilisateur qu'elle n'est plus disponible.
//            if (context.HttpContext.Request.Path.Value.Contains("api"))
//            {
//                // Renvoyé à l'utilisateur l'indisponibilité.
//                context.Result = new BadRequestObjectResult(
//                    new
//                    {
//                       Result = new[] {"Cette méthode n'est plus disponible.."}
//                    });
//            }
//        }
//    }
//}
