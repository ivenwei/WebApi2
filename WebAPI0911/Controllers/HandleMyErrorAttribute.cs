using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebAPI0911.Controllers
{
    public class HandleMyErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                    new MyError()
                    {
                        Error_Message = "Error",
                        SubStatusCode = "123"
                    }
                );
        }
    }
}