using Microsoft.AspNetCore.Mvc;

namespace valet.lib.Auth.Service.Token.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateUserAttribute : TypeFilterAttribute
    {
        public ValidateUserAttribute() : base(typeof(ValidateUserFilter)) { }

        public string Roles
        {
            get => (string)(Arguments?[0] ?? "");
            set => Arguments = [value];
        }
    }
}
