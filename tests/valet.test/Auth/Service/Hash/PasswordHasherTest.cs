using valet.lib.Auth.Service.Hash;

namespace valet.test.Auth.Service.Hash
{
    public class PasswordHasherTest
    {
        [Fact]
        public void Should_Return_Right_Given_Password()
        {
            var password = "Abcd#1234";
            var hasher = new PasswordHasher();
            var result = hasher.HashPassword(password);

            Assert.NotEqual(password, result);
            Assert.True(hasher.VerifyPassword(password, result));
        }

        [Fact]
        public void Should_Return_Wrong_Given_Password()
        {
            var password = "Abcd#1234";
            var hasher = new PasswordHasher();
            var result = hasher.HashPassword(password);

            Assert.NotEqual(password, result);
            Assert.False(hasher.VerifyPassword("abcd1234", result));
        }
    }
}
