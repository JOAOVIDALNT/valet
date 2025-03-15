using Bogus;
using valet.lib.Auth.Domain.Entities;

namespace valet.test.Builders
{
    public static class RoleBuilder
    {
        public static Role Build()
        {
            return new Faker<Role>()
                .RuleFor(x => x.Name, "tester")
                .RuleFor(x => x.Id, (x) => x.Random.Guid());
        } 
    }
}
