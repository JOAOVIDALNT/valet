using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Data;
using valet.lib.Auth.Data.Repositories;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Core.Data.Repositories;
using valet.test.Builders;

namespace valet.test.Auth.Data.Repositories
{
    public class UserRepositoryTest
    {
        private DbContextOptions options;
        public UserRepositoryTest()
        {
            options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;
        }
        [Fact]
        public async Task Should_Create_User()
        {
            using (var context = new AuthDbContext(options))
            {
                context.Database.EnsureCreated();

                var userRepository = new UserRepository<AuthDbContext>(context);
                var uow = new UnitOfWork<AuthDbContext>(context);

                var user = UserBuilder.Build();

                await userRepository.CreateAsync(user);
                await uow.CommitAsync();

                var result = await userRepository
                    .GetAsync(q => q
                        .Where(x => x.Id == user.Id));

                Assert.NotNull(result);
                Assert.Equal(result.Login, user.Login);
            }
        }

        [Fact]
        public async Task Should_Return_User_Exists()
        {
            using (var context = new AuthDbContext(options))
            {
                context.Database.EnsureCreated();

                var userRepository = new UserRepository<AuthDbContext>(context);
                var uow = new UnitOfWork<AuthDbContext>(context);

                var user = UserBuilder.Build();

                await userRepository.CreateAsync(user);
                await uow.CommitAsync();

                var result = await userRepository.UserExistsAsync(user.Login);

                Assert.True(result);
            }
        }

        [Fact]
        public async Task Should_Return_User_Not_Exists()
        {
            using (var context = new AuthDbContext(options))
            {
                context.Database.EnsureCreated();

                var userRepository = new UserRepository<AuthDbContext>(context);

                var user = UserBuilder.Build();

                var result = await userRepository.UserExistsAsync(user.Login);

                Assert.False(result);
            }
        }

        [Fact]
        public async Task Should_Return_User_With_Roles()
        {
            using (var context = new AuthDbContext(options))
            {
                context.Database.EnsureCreated();

                var userRepository = new UserRepository<AuthDbContext>(context);
                var uow = new UnitOfWork<AuthDbContext>(context);

                var user = UserBuilder.Build();
                var role = new Role("tester");
                var userRole = UserRoleBuilder.Build(user, role);

                user.UserRoles.Add(userRole);

                await userRepository.CreateAsync(user);
                await uow.CommitAsync();

                var result = await userRepository.GetUserWithRolesAsync(user.Login);

                Assert.NotNull(result);
                
                Assert.Equal(user.Login, result.Login);
                Assert.Equal(userRole, result.UserRoles.First());
            }
        }
    }
}
