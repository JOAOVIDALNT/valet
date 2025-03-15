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
                .RuleFor(x => x.Id, (x) => x.Random.Guid())
                .RuleFor(x => x.FirstName, (x) => x.Person.FirstName)
                .RuleFor(x => x.LastName, (x) => x.Person.LastName)
                .RuleFor(x => x.Email, (x, y) => x.Internet.Email(y.FirstName + y.LastName))
                .RuleFor(x => x.Password, (x) => hasher.HashPassword(x.Internet.Password()));
        }
    }
}
