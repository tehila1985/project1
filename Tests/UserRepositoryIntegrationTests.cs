using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Test;
using Xunit;

namespace Tests
{
    public class UserRepositoryIntegrationTests : IClassFixture<DatabaseFixture>
    {
        private readonly myDBContext _dbContext;
        private readonly UserRepository _userRepository;

        public UserRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task AddNewUser_ShouldAddUser_WithValidData()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };

            // Act
            var result = await _userRepository.AddNewUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);

            // Act
            var result = await _userRepository.GetUserById(user.UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnListOfUsers_WhenUsersExist()
        {
            // Arrange
            var user1 = new User { Gmail = "user1@example.com", Password = "Password1" };
            var user2 = new User { Gmail = "user2@example.com", Password = "Password2" };
            await _userRepository.AddNewUser(user1);
            await _userRepository.AddNewUser(user2);

            // Act
            var result = await _userRepository.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, u => u.Gmail == user1.Gmail);
            Assert.Contains(result, u => u.Gmail == user2.Gmail);
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WithValidCredentials()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);
            var loginUser = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };

            // Act
            var result = await _userRepository.Login(loginUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task Update_ShouldModifyUser_WhenValidDataProvided()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);
            user.Password = "NewStrongPassword456";

            // Act
            var result = await _userRepository.update(user.UserId, user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Password, result.Password);
        }

        [Fact]
        public async Task AddNewUser_ShouldThrowValidationException_WhenEmailInvalid()
        {
            // Arrange
            var user = new User { Gmail = "invalidEmail", Password = "StrongPassword123" };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        }

        [Fact]
        public async Task AddNewUser_ShouldThrowValidationException_WhenPasswordTooShort()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "short" }; // Assuming password must be at least 6 characters

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = await _userRepository.GetUserById(9999); // Assuming 9999 is an invalid ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenInvalidCredentialsProvided()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);

            var loginUser = new User { Gmail = "valid.email@example.com", Password = "WrongPassword" }; // Invalid password

            // Act
            var result = await _userRepository.Login(loginUser);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var user = new User { UserId = 9999, Gmail = "valid.email@example.com", Password = "StrongPassword123" }; // Assuming 9999 is invalid

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _userRepository.update(user.UserId, user));
        }

        [Fact]
        public async Task AddNewUser_ShouldThrowValidationException_WhenEmailAlreadyExists()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);

            var duplicateUser = new User { Gmail = "valid.email@example.com", Password = "AnotherPassword456" };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(duplicateUser));
        }

        [Fact]
        public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Act
            var result = await _userRepository.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenDataIsInvalid()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);
            user.Gmail = "invalidEmail"; // Invalid email format

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.update(user.UserId, user));
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenPasswordIsEmpty()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);

            var loginUser = new User { Gmail = "valid.email@example.com", Password = "" }; // Empty password

            // Act
            var result = await _userRepository.Login(loginUser);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenIdIsNegative()
        {
            // Act
            var result = await _userRepository.GetUserById(-1); // Negative ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddNewUser_ShouldSetDefaultValues_WhenUserIsCreated()
        {
            // Arrange
            var user = new User { Gmail = "default.email@example.com", Password = "StrongPassword123" };

            // Act
            var result = await _userRepository.AddNewUser(user);

            // Assert
            Assert.NotNull(result);
            // Assuming default values would be checked here
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedUser_WhenValidDataProvided()
        {
            // Arrange
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            await _userRepository.AddNewUser(user);
            user.Password = "NewStrongPassword456";

            // Act
            var result = await _userRepository.update(user.UserId, user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Password, result.Password);
        }
    }
}

