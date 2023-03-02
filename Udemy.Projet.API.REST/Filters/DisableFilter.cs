//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace Projet.API.REST.Swagger.Filters
//{
//    /// <summary>
//    /// intercepte les requêtes qui contiennent le chemin "api"
//    /// dans l'URL de la requête et retourne une réponse BadRequest
//    /// avec un message d'erreur indiquant que la méthode n'est plus disponible.
//    /// Cela peut être utile dans certaines situations, par exemple lorsque tu veux désactiver
//    /// temporairement certaines fonctionnalités de l'API sans supprimer complètement
//    /// les endpoints correspondants.
//    /// </summary>
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
//                        Result = new[] { "Cette méthode n'est plus disponible.." }
//                    });
//            }
//        }
//    }
//}
