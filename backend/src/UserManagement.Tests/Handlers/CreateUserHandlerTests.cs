using FluentAssertions;
using Moq;
using UserManagement.Application.Features.Users.Commands;
using UserManagement.Application.Features.Users.Handlers;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Tests.Handlers;

public class CreateUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();

        _handler = new CreateUserHandler(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenEmailDoesNotExist()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Name = "Vinicius",
            Email = "vinicius@email.com",
            Password = "Senha@123"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        _passwordHasherMock
            .Setup(h => h.HashPassword(command.Password))
            .Returns("hashed-password");

        _userRepositoryMock
            .Setup(r => r.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _userRepositoryMock
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();

        _userRepositoryMock.Verify(r => r.InsertAsync(It.Is<User>(u =>
            u.Name == command.Name &&
            u.Email == command.Email &&
            u.PasswordHash == "hashed-password"
        ), It.IsAny<CancellationToken>()), Times.Once);

        _userRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        var existingUser = new User("Existente", "existente@email.com");

        var command = new CreateUserCommand
        {
            Name = "Novo",
            Email = "existente@email.com",
            Password = "SenhaNova@123"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Email já cadastrado.");

        _userRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _userRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}