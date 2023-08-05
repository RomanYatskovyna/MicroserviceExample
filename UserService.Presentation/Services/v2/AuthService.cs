using Grpc.Core;
using UserService.Grpc.v2;

namespace UserService.Presentation.Services.v2;

public class AuthService : Auth.AuthBase
{
    public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        return Task.FromResult(new LoginResponse()
        {
            JwtToken = request.Username
        });
    }
}