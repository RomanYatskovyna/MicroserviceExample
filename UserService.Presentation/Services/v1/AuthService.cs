using Google.Api;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using UserService.Grpc.v1;

namespace UserService.Presentation.Services.v1;

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
