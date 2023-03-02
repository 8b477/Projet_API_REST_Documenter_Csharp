using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Projet.API.REST.Swagger.Models;

namespace Projet.API.REST.Swagger.Filters
{
    /// <summary>
    /// Permet de reformater le résultat d'une requête à la sortie.
    /// </summary>
    public class FormattingResultFilter : IAsyncResultFilter
    {
        /// <summary>
        /// Construction d'un objet sur base de ma classe HttpResult.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            
            if (context.Result is not EmptyResult)
            {
                var item = context.Result as ObjectResult;

                context.Result = new JsonResult(new HttpResult<object>()
                {
                    StatutCode = int.TryParse(item?.StatusCode.ToString(), out int result) ? result : 500,
                    IsSucced = (item?.StatusCode == 200 || item?.StatusCode == 201) ? true : false,
                    Data = (item != null) ? item.Value : null
                });
            }

            var _ = await next();
        }
    }
}
