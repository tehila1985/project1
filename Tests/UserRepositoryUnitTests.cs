using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model;
using Moq;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UserRepositoryUnitTests
    {
        private readonly Mock<DbSet<User>> mockSet;
        private readonly Mock<myDBContext> mockContext;
        private readonly UserRepository userRepository;

        public UserRepositoryUnitTests()
        {
            mockSet = new Mock<DbSet<User>>();
            mockContext = new Mock<myDBContext>();
            userRepository = new UserRepository(mockContext.Object);
        }
        [Fact]
        public async Task AddNewUser_ShouldAddUser_WithValidData()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };

            var mockEntry = new Mock<EntityEntry<User>>();
            mockSet.Setup(m => m.AddAsync(It.IsAny<User>(), default))
                   .ReturnsAsync(mockEntry.Object);

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await userRepository.AddNewUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            var user = new User { UserId = 1, Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            var data = new List<User> { user }.AsQueryable();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var result = await userRepository.GetUserById(user.UserId);

            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnListOfUsers_WhenUsersExist()
        {
            var user1 = new User { Gmail = "user1@example.com", Password = "Password1" };
            var user2 = new User { Gmail = "user2@example.com", Password = "Password2" };
            var users = new List<User> { user1, user2 }.AsQueryable();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var result = await userRepository.GetUsers();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Gmail == user1.Gmail);
            Assert.Contains(result, u => u.Gmail == user2.Gmail);
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WithValidCredentials()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            var data = new List<User> { user }.AsQueryable();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var loginUser = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            var result = await userRepository.Login(loginUser);

            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task Update_ShouldModifyUser_WhenValidDataProvided()
        {
            var user = new User { UserId = 1, Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            mockSet.Setup(m => m.Update(It.IsAny<User>()));
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            user.Password = "NewStrongPassword456";

            var result = await userRepository.update(user.UserId, user);

            Assert.NotNull(result);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task AddNewUser_ShouldThrowValidationException_WhenEmailInvalid()
        {
            var user = new User { Gmail = "invalidEmail", Password = "StrongPassword123" };

            await Assert.ThrowsAsync<DbUpdateException>(async () => await userRepository.AddNewUser(user));
        }

        [Fact]
        public async Task AddNewUser_ShouldThrowValidationException_WhenPasswordTooShort()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "short" };

            await Assert.ThrowsAsync<DbUpdateException>(async () => await userRepository.AddNewUser(user));
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var result = await userRepository.GetUserById(9999);
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenInvalidCredentialsProvided()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await userRepository.AddNewUser(user);

            var loginUser = new User { Gmail = "valid.email@example.com", Password = "WrongPassword" };
            var result = await userRepository.Login(loginUser);

            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenUserNotFound()
        {
            var user = new User { UserId = 9999, Gmail = "valid.email@example.com", Password = "StrongPassword123" };

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await userRepository.update(user.UserId, user));
        }

        [Fact]
        public async Task AddNewUser_ShouldThrowValidationException_WhenEmailAlreadyExists()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await userRepository.AddNewUser(user);

            var duplicateUser = new User { Gmail = "valid.email@example.com", Password = "AnotherPassword456" };
            await Assert.ThrowsAsync<DbUpdateException>(async () => await userRepository.AddNewUser(duplicateUser));
        }

        [Fact]
        public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            var result = await userRepository.GetUsers();
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenDataIsInvalid()
        {
            var user = new User { UserId = 1, Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await userRepository.AddNewUser(user);
            user.Gmail = "invalidEmail";

            await Assert.ThrowsAsync<DbUpdateException>(async () => await userRepository.update(user.UserId, user));
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenPasswordIsEmpty()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await userRepository.AddNewUser(user);

            var loginUser = new User { Gmail = "valid.email@example.com", Password = "" };
            var result = await userRepository.Login(loginUser);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenIdIsNegative()
        {
            var result = await userRepository.GetUserById(-1);
            Assert.Null(result);
        }

        [Fact]
        public async Task AddNewUser_ShouldSetDefaultValues_WhenUserIsCreated()
        {
            var user = new User { Gmail = "default.email@example.com", Password = "StrongPassword123" };
            var result = await userRepository.AddNewUser(user);
            Assert.NotNull(result);
            // Check for default values if applicable
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedUser_WhenValidDataProvided()
        {
            var user = new User { UserId = 1, Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await userRepository.AddNewUser(user);
            user.Password = "NewStrongPassword456";

            var result = await userRepository.update(user.UserId, user);
            Assert.NotNull(result);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task AddNewUser_ShouldThrowException_WhenUserIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await userRepository.AddNewUser(null));
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenUserDoesNotExist()
        {
            var user = new User { UserId = 9999, Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await userRepository.update(user.UserId, user));
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenGmailIsEmpty()
        {
            var loginUser = new User { Gmail = "", Password = "StrongPassword123" };
            var result = await userRepository.Login(loginUser);
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var loginUser = new User { Gmail = "not.existing@example.com", Password = "SomePassword" };
            var result = await userRepository.Login(loginUser);
            Assert.Null(result);
        }
    }
}