using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Services;
using UserService.Domain.Entities;
using UserService.Grpc.v1;
using UserService.Infrastructure.Database;
using UserService.Infrastructure.Services;

namespace UserService.Presentation.Services.v1;

public sealed class AuthService : Auth.AuthBase
{
    private readonly ITokenService _tokenService;
    private readonly UserServiceDbContext _context;

    public AuthService(ITokenService tokenService, UserServiceDbContext context)
    {
        _tokenService = tokenService;
        _context = context;
    }

    public override async Task<TokenResponse> Login(LoginRequest request, ServerCallContext context)
    {
        var foundUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        if (foundUser is null || !PasswordEncrypter.VerifyPassword(request.Password, foundUser.PasswordHash))
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Email or password is incorrect"));
        return new TokenResponse
        {
            AccessToken = _tokenService.GenerateAccessToken(request.Email, foundUser.RoleName),
            RefreshToken = _tokenService.GenerateRefreshToken(request.Email),
        };
    }
    public override async Task<TokenResponse> Refresh(RefreshRequest request, ServerCallContext context)
    {
        try
        {
            var validationResult = _tokenService.ValidateRefreshToken(request.RefreshToken);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == validationResult.Identity!.Name);
            return new TokenResponse
            {
                AccessToken = _tokenService.GenerateAccessToken(user.Email, user.RoleName),
                RefreshToken = _tokenService.GenerateRefreshToken(user.Email),
            };
        }
        catch (SecurityTokenExpiredException e)
        {
            throw new RpcException(new Status(StatusCode.PermissionDenied, e.Message, e));
        }
        catch (SecurityTokenException e)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, e.Message, e));
        }
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        var role = await _context.Roles.FirstAsync();
        var user = new User()
        {
            Email = request.Email,
            PasswordHash = PasswordEncrypter.HashPassword(request.Password),
            RoleName = role.Name,
        };
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return new RegisterResponse
        {
            Message = "Registration Completed Successfully"
        };
    }

}
