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
                    var firstName = f.Name.FirstName();
                    var lastName = f.Name.LastName();
                    var email = f.Internet.Email(firstName, lastName);
                    var password = f.Internet.Password(8, true, "#");
                    return new User(firstName, lastName, email, hasher.HashPassword(password));
                });
        }
    }
}
