using Grpc.Core;
using Grpc.Net.Client;
using System.Net;
using UserService.Grpc.v1;

namespace UserService.Grpc;
public class UserServiceClient
{
    public string Address
    {
        get
        {
            UpdateCredentials();
            return _address;
        }
        set => _address = value;
    }

    private readonly GrpcChannelOptions _channelOptions;
    private Auth.AuthClient _authClient;
    private string _address;
    private const int TimeoutInSeconds = 10;
    public UserServiceClient(string address, int exponentialBackoffLimitInSeconds = 120)
    {
        Address = address;
        _channelOptions = new GrpcChannelOptions
        {

            MaxReconnectBackoff = TimeSpan.FromSeconds(exponentialBackoffLimitInSeconds),
            InitialReconnectBackoff = default,
        };
    }

    public void UpdateCredentials()
    {
        _authClient = new Auth.AuthClient(GrpcChannel.ForAddress(Address, _channelOptions));

    }
    public TokenResponse Login(LoginRequest loginRequest, int requestTimeoutInSeconds = TimeoutInSeconds, CancellationToken cancellationToken = default)
    {
        var loginResponse = _authClient.Login(loginRequest, null, DateTime.UtcNow.AddSeconds(requestTimeoutInSeconds), cancellationToken);
        UpdateCredentials(loginResponse);
        return loginResponse;
    }
    public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest, int requestTimeoutInSeconds = TimeoutInSeconds, CancellationToken cancellationToken = default)
    {
        var loginResponse = await _authClient.LoginAsync(loginRequest, null, DateTime.UtcNow.AddSeconds(requestTimeoutInSeconds), cancellationToken);
        UpdateCredentials(loginResponse);
        return loginResponse;
    }

    private void UpdateCredentials(TokenResponse token)
    {
        var credentials = CallCredentials.FromInterceptor((context, metadata) =>
        {
            metadata.Add("Authorization", $"Bearer {token.AccessToken}");
            return Task.CompletedTask;
        });
        _channelOptions.Credentials = ChannelCredentials.Create(new SslCredentials(), credentials);
    }
}
