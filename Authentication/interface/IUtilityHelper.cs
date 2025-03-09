using Microsoft.AspNetCore.Http;

namespace Farm.Controllers
{
    public interface IUtilityHelper
    {
        public string GetUserFromRequest(HttpRequest request);
    }
}