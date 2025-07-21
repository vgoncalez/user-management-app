using MediatR;
using UserManagement.Application.Features.Users.Responses;

namespace UserManagement.Application.Features.Users.Queries;

public class GetAllUserQuery : IRequest<List<UserResponse>>
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
