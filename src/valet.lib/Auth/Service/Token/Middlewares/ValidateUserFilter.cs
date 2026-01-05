using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using valet.lib.Auth.Domain.Interfaces;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Exception;
using valet.lib.Core.Exception.Resource;
using valet.lib.Core.Exception.Response;

namespace valet.lib.Auth.Service.Token.Middlewares
{
    public class ValidateUserFilter : IAsyncAuthorizationFilter
    {
        private readonly ITokenValidator _tokenValidator;
        private readonly IUserRepository _userRepository;
        private readonly string _roles;
        public ValidateUserFilter(IUserRepository userRepository, ITokenValidator tokenValidator, string roles)
        {
            _userRepository = userRepository;
            _tokenValidator = tokenValidator;
            _roles = roles;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var user = context.HttpContext.User;
                var token = ExtractToken(context);

                var userIdentifier = _tokenValidator.ValidateAndGetUserIdentifier(token);

                if(!await _userRepository.UserExistsAsync(userIdentifier))
                    throw new UnauthorizedException(ValetResourceMessageException.USER_UNAUTHORIZED);

                if (!string.IsNullOrWhiteSpace(_roles))
                {
                    var requiredRoles = _roles.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var userRoles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

                    if (!requiredRoles.Any(role => userRoles.Contains(role)))
                        throw new ForbiddenException(ValetResourceMessageException.USER_FORBIDDEN);
                }

            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorResponse(ValetResourceMessageException.TOKEN_EXPIRED)
                {
                    TokenIsExpired = true
                });
            }
            catch (BaseException ex)
            {
                context.HttpContext.Response.StatusCode = (int)ex.GetStatusCode();
                context.Result = new ObjectResult(new ErrorResponse(ex.GetErrorMessages()));
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ErrorResponse(ValetResourceMessageException.USER_UNAUTHORIZED));
            }
        }

        private static string ExtractToken(AuthorizationFilterContext context)
        {
            var header = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(header))
                throw new UnauthorizedException(ValetResourceMessageException.NO_TOKEN);

            return header["Bearer ".Length..].Trim();
                
        }
    }
}
