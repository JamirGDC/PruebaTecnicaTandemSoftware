using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaTandemSoftware.Application.Common.Result;

namespace PruebaTecnicaTandemSoftware.Application.Common.Filters;

public class ResultHttpCodeFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is ResultBase resultWithStatus)
        {
            context.HttpContext.Response.StatusCode = (int)resultWithStatus.HttpStatusCode;
            return new JsonResult(resultWithStatus)
            {
                StatusCode = (int)resultWithStatus.HttpStatusCode,
                ContentType = "application/json"
            };
        }
        return result;
    }
}