using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Farm.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _someFilterParameter;

        public AuthorizeRoleAttribute(string someFilterParameter)
        {
            _someFilterParameter = someFilterParameter;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // var user = context.HttpContext.User;

            // if (!user.Identity.IsAuthenticated)
            // {
            //     // it isn't needed to set unauthorized result 
            //     // as the base class already requires the user to be authenticated
            //     // this also makes redirect to a login page work properly
            //     // context.Result = new UnauthorizedResult();
            //     return;
            // }

            // // you can also use registered services
            // var someService = context.HttpContext.RequestServices.GetService<ISomeService>();

            // var isAuthorized = someService.IsUserAuthorized(user.Identity.Name, _someFilterParameter);
            // if (!isAuthorized)
            // {
            //     context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            //     return;
            // }
        }
    }
}