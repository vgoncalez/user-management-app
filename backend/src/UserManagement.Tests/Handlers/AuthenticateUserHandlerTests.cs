using FluentAssertions;
using Moq;
using UserManagement.Application.Features.Auth.Handlers;
using UserManagement.Application.Features.Auth.Queries;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Tests.Handlers;

public class AuthenticateUserHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var user = new User("Vinicius", "vinicius@email.com");
        user.SetPasswordHash("hashed-password");

        var mockRepo = new Mock<IUserRepository>();
        var mockHasher = new Mock<IPasswordHasher>();
        var mockToken = new Mock<ITokenProvider>();

        mockRepo.Setup(r => r.GetByEmailAsync(user.Email, cancellationToken)).ReturnsAsync(user);
        mockHasher.Setup(h => h.VerifyPassword("123456", user.PasswordHash)).Returns(true);
        mockToken.Setup(t => t.Create(user)).Returns("fake-jwt-token");

        var handler = new AuthenticateUserHandler(mockRepo.Object, mockHasher.Object, mockToken.Object);

        var query = new AuthenticateUserQuery
        {
            Email = user.Email,
            Password = "123456"
        };

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().Be("fake-jwt-token");
        result.User.Should().NotBeNull();
        result.User.Email.Should().Be(user.Email);
    }

    [Fact]
    public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenUserNotFound()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var mockRepo = new Mock<IUserRepository>();
        var mockHasher = new Mock<IPasswordHasher>();
        var mockToken = new Mock<ITokenProvider>();

        mockRepo.Setup(r => r.GetByEmailAsync("invalido@email.com", cancellationToken)).ReturnsAsync((User)null);

        var handler = new AuthenticateUserHandler(mockRepo.Object, mockHasher.Object, mockToken.Object);

        var query = new AuthenticateUserQuery
        {
            Email = "invalido@email.com",
            Password = "123456"
        };

        // Act
        Func<Task> act = async () => await handler.Handle(query, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Credenciais inválidas.");
    }

    [Fact]
    public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenPasswordIsInvalid()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var user = new User("Vinicius", "vinicius@email.com");
        user.SetPasswordHash("hashed-password");

        var mockRepo = new Mock<IUserRepository>();
        var mockHasher = new Mock<IPasswordHasher>();
        var mockToken = new Mock<ITokenProvider>();

        mockRepo.Setup(r => r.GetByEmailAsync(user.Email, cancellationToken)).ReturnsAsync(user);
        mockHasher.Setup(h => h.VerifyPassword("senhaerrada", user.PasswordHash)).Returns(false);

        var handler = new AuthenticateUserHandler(mockRepo.Object, mockHasher.Object, mockToken.Object);

        var query = new AuthenticateUserQuery
        {
            Email = user.Email,
            Password = "senhaerrada"
        };

        // Act
        Func<Task> act = async () => await handler.Handle(query, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Credenciais inválidas.");
    }
}
