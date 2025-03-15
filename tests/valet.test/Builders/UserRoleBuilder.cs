using valet.lib.Auth.Domain.Entities;

namespace valet.test.Builders
{
    public static class UserRoleBuilder
    {
        public static UserRole Build(User user, Role role)
        {
            return new UserRole
            {
                Id = Guid.NewGuid(),
                User = user,
                Role = role,
                UserId = user.Id,
                RoleId = role.Id
            };
        }
    }
}
