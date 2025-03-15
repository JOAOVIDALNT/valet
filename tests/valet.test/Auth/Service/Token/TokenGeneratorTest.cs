using Bogus;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Service.Hash;
using valet.lib.Auth.Service.Token;
using valet.test.Builders;

namespace valet.test.Auth.Service.Token
{
    public class TokenGeneratorTest
    {
        [Fact]
        public void Should_Generate_Valid_Token()
        {
            var generator = new TokenGenerator("u9yw0akS1fwQh09fA4rpR7ob2t11if41");

            var user = UserBuilder.Build();

            var result = generator.GenerateToken(user);

            Assert.StartsWith("ey", result);
        }

        [Fact]
        public void Should_Return_Valid_Token()
        {
            var generator = new TokenGenerator("u9yw0akS1fwQh09fA4rpR7ob2t11if41");

            var user = UserBuilder.Build();
            var result = generator.GenerateToken(user);
            //user.FirstName = "xxx";

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(result) as JwtSecurityToken;
            var claims = jsonToken?.Claims;

            Assert.Contains(claims, x => x.Value == user.FirstName);
        }

        [Fact]
        public void Should_Return_Invalid_Token()
        {
            var generator = new TokenGenerator("u9yw0akS1fwQh09fA4rpR7ob2t11if41");

            var user = UserBuilder.Build();
            var result = generator.GenerateToken(user);
            user.FirstName = "xxx";

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(result) as JwtSecurityToken;
            var claims = jsonToken?.Claims;

            Assert.DoesNotContain(claims, x => x.Value == user.FirstName);
        }
    }
}
