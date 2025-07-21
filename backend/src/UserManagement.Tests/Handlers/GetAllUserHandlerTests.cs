using FluentAssertions;
using Moq;
using UserManagement.Application.Features.Users.Handlers;
using UserManagement.Application.Features.Users.Queries;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.Tests.Handlers;

public class GetAllUserHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFilteredUsers_WhenUsersExist()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var users = new List<User>
        {
            new User("Vinicius", "vinicius@email.com"),
            new User("Goncalez", "goncalez@email.com")
        };

        var mockRepository = new Mock<IUserRepository>();
        mockRepository
            .Setup(r => r.GetAllAsync("Vinicius", "vinicius@email.com", cancellationToken))
            .ReturnsAsync(users.Where(u => u.Name == "Vinicius" && u.Email == "vinicius@email.com").ToList());

        var handler = new GetAllUserHandler(mockRepository.Object);

        var query = new GetAllUserQuery
        {
            Name = "Vinicius",
            Email = "vinicius@email.com"
        };

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Vinicius");
        result.First().Email.Should().Be("vinicius@email.com");
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoUsersMatch()
    {
        // Arrange
        var cancellationToken = new CancellationToken();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository
            .Setup(r => r.GetAllAsync("Inexistente", "naoexiste@email.com", cancellationToken))
            .ReturnsAsync(new List<User>());

        var handler = new GetAllUserHandler(mockRepository.Object);

        var query = new GetAllUserQuery
        {
            Name = "Inexistente",
            Email = "naoexiste@email.com"
        };

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.Should().BeEmpty();
    }
}