namespace UserService.Tests.Integration.Tests.Auth;
public class LoginTests : BaseTest
{
	[Fact]
	public async Task LoginAsync_returns_Token_when_ok()
	{
		//Arrange
		var request = new LoginRequest
		{
			Email = "Test",
			Password = "Test"
		};
		//Act
		var response = await Client.LoginAsync(request);
		response.AccessToken.Should().NotBeNullOrEmpty();
	}
}
