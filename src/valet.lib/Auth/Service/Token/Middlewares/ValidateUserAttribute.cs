using Microsoft.AspNetCore.Mvc;

namespace valet.lib.Auth.Service.Token.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateUserAttribute : TypeFilterAttribute
    {
        public ValidateUserAttribute(string roles = "") : base(typeof(ValidateUserFilter)) => Arguments = [roles];
    }
}
