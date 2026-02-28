using Bogus;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Service.Hash;

namespace valet.test.Builders
{
    public static class UserBuilder
    {
        public static User Build()
        {
            var hasher = new PasswordHasher();
            return new Faker<User>()
                .CustomInstantiator(f =>
                {
                    var email = f.Internet.Email();
                    var password = f.Internet.Password(8, true, "#");
                    return new User(email, hasher.HashPassword(password));
                });
        }
    }
}
