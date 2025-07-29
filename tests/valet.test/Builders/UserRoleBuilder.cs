using valet.lib.Auth.Domain.Entities;

namespace valet.test.Builders
{
    public static class UserRoleBuilder
    {
        public static UserRole Build(User user, Role role)
        {
            return new UserRole(user, role);
        }
    }
}
