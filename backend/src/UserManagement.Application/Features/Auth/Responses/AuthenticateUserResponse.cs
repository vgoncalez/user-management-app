using UserManagement.Application.Features.Users.Responses;

namespace UserManagement.Application.Features.Auth.Responses;

public class AuthenticateUserResponse
{
    public AuthenticateUserResponse()
    {
    }

    public AuthenticateUserResponse(UserResponse user, string accessToken)
    {
        User = user;
        AccessToken = accessToken;
    }

    public UserResponse User { get; set; } = new UserResponse();

    public string AccessToken { get; set; } = string.Empty;
}
