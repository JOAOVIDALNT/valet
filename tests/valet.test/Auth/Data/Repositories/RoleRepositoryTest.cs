using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Data;
using valet.lib.Auth.Data.Repositories;
using valet.lib.Core.Data.Repositories;
using valet.test.Builders;

namespace valet.test.Auth.Data.Repositories
{
    public class RoleRepositoryTest
    {
        private DbContextOptions options;
        public RoleRepositoryTest()
        {
            options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;
        }


        [Fact]
        public async Task Should_Return_Role_Exists()
        {
            using (var context = new AuthDbContext(options))
            {
                context.Database.EnsureCreated();

                var roleRepository = new RoleRepository<AuthDbContext>(context);
                var uow = new UnitOfWork<AuthDbContext>(context);

                var role = RoleBuilder.Build();

                await roleRepository.CreateAsync(role);
                await uow.Commit();

                var result = await roleRepository.RoleExistsAsync(role.Name);

                Assert.True(result);
            }
        }

        [Fact]
        public async Task Should_Return_Role_Not_Exists()
        {
            using (var context = new AuthDbContext(options))
            {
                context.Database.EnsureCreated();

                var roleRepository = new RoleRepository<AuthDbContext>(context);

                var role = RoleBuilder.Build();

                var result = await roleRepository.RoleExistsAsync(role.Name);

                Assert.False(result);
            }
        }
    }
}
