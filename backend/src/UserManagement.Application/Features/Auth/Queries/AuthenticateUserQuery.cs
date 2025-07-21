using MediatR;
using UserManagement.Application.Features.Auth.Responses;

namespace UserManagement.Application.Features.Auth.Queries;

public class AuthenticateUserQuery : IRequest<AuthenticateUserResponse>
{
    public AuthenticateUserQuery()
    {
    }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
