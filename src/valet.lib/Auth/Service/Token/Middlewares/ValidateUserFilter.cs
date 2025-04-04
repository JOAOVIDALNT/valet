using Microsoft.AspNetCore.Mvc.Filters;
using valet.lib.Auth.Domain.Interfaces.Repositories;

namespace valet.lib.Auth.Service.Token.Middlewares
{
    public class ValidateUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IUserRepository _userRepository;
        private readonly string _roles;
        public ValidateUserFilter(IUserRepository userRepository, string roles)
        {
            _userRepository = userRepository;
            _roles = roles;
        }
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = "";

            throw new NotImplementedException();
        }

        private static string ExtractToken(AuthorizationFilterContext context)
        {
            var header = context.HttpContext.Request.Headers.Authorization.ToString();

            //if (string.IsNullOrWhiteSpace(header))
            return "";
                
        }
    }
}
