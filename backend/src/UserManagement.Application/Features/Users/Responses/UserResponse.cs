using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Responses;

public class UserResponse
{
    public UserResponse()
    {
    }

    public UserResponse(User user)
    {
        this.Id = user.Id;
        this.Name = user.Name;
        this.Email = user.Email;
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
