using MediatR;
using UserManagement.Application.Features.Auth.Queries;
using UserManagement.Application.Features.Auth.Responses;
using UserManagement.Application.Features.Users.Responses;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Interfaces.Repositories;

namespace UserManagement.Application.Features.Auth.Handlers;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserQuery, AuthenticateUserResponse>
{
    private readonly IUserRepository _repository;

    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public AuthenticateUserHandler(
        IUserRepository repository,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthenticateUserResponse> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        string accessToken = _tokenProvider.Create(user);

        var userResponse = new UserResponse(user);
        var response = new AuthenticateUserResponse(userResponse, accessToken);

        return response;
    }
}
