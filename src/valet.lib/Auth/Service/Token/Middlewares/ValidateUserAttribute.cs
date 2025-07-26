using Microsoft.AspNetCore.Mvc;

namespace valet.lib.Auth.Service.Token.Middlewares
{
    /// <summary>
    /// Attribute to enforce user validation and role-based authorization on controllers or actions.
    /// </summary>
    /// <remarks>
    /// This attribute applies the <see cref="ValidateUserFilter"/> authorization filter,
    /// which validates the presence and validity of a JWT token,
    /// checks if the user exists, and optionally verifies if the user has required roles.
    /// </remarks>
    /// <param name="roles">
    /// A comma-separated list of roles required to access the resource.
    /// If empty or omitted, only token validation and user existence are enforced.
    /// </param>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateUserAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserAttribute"/> class.
        /// </summary>
        /// <param name="roles">Comma-separated roles required to access the resource.</param>
        public ValidateUserAttribute(string roles = "") : base(typeof(ValidateUserFilter)) => Arguments = [roles];
    }
}
