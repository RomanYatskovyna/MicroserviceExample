using Grpc.Core;
using Grpc.Net.Client;

namespace UserService.Grpc;
//public class UserServiceClient
//{
//	private readonly GrpcChannelOptions _channelOptions;
//	private Auth.AuthClient _authClient;
//	private const int TimeoutInSeconds = 10;
//	public UserServiceClient(string address, int exponentialBackoffLimitInSeconds = 120)
//	{

//		_channelOptions = new GrpcChannelOptions
//		{

//			MaxReconnectBackoff = TimeSpan.FromSeconds(exponentialBackoffLimitInSeconds),
//			InitialReconnectBackoff = default,
//		};
//		var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
//		{

//			MaxReconnectBackoff = TimeSpan.FromSeconds(exponentialBackoffLimitInSeconds),
//			InitialReconnectBackoff = default,
//		});
//		_authClient = new Auth.AuthClient(channel);

//	}
//	public LoginResponse Login(LoginRequest loginRequest, int requestTimeoutInSeconds = TimeoutInSeconds, CancellationToken cancellationToken = default)
//	{
//		var loginResponse = _authClient.Login(loginRequest, null, DateTime.UtcNow.AddSeconds(requestTimeoutInSeconds), cancellationToken);
//		UpdateCredentials(loginResponse.JwtToken);
//		return loginResponse;
//	}
//	public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest, int requestTimeoutInSeconds = TimeoutInSeconds, CancellationToken cancellationToken = default)
//	{
//		var loginResponse = await _authClient.LoginAsync(loginRequest, null, DateTime.UtcNow.AddSeconds(requestTimeoutInSeconds), cancellationToken);
//		UpdateCredentials(loginResponse.JwtToken);
//		return loginResponse;
//	}

//	private void UpdateCredentials(string jwtToken)
//	{
//		var credentials = CallCredentials.FromInterceptor((context, metadata) =>
//		{
//			metadata.Add("Authorization", $"Bearer {jwtToken}");
//			return Task.CompletedTask;
//		});
//		_channelOptions.Credentials = ChannelCredentials.Create(new SslCredentials(), credentials);
//	}

//}
