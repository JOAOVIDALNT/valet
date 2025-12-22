using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace valet.lib.Auth.Service.Token
{
    internal abstract class TokenHandler
    {
        protected static SymmetricSecurityKey SecurityKey(string signingKey)
        {
            var bytes = Encoding.UTF8.GetBytes(signingKey);

            return new SymmetricSecurityKey(bytes);
        }
    }
}
