using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Projet.API.REST.Swagger.Models;

namespace Projet.API.REST.Swagger.Filters
{
    public class FormattingResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Permet de reformater le résultat d'une requête à la sortie
            if (!(context.Result is EmptyResult))
            {
                var item = context.Result as ObjectResult;

                context.Result = new JsonResult(new HttpResult<object>()
                {
                    StatutCode = (int)item.StatusCode,
                    IsSucced = (item.StatusCode == 200 || item.StatusCode == 201) ? true : false,
                    Data = item.Value
                });
            }

            var resultContext = await next();
        }
    }
}
