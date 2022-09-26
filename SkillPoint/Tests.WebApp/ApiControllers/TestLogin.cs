using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Public.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Tests.WebApp.ApiControllers;

public class TestLogin : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public TestLogin(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }
        );
    }
    
    [Fact]
    async Task RegisterApiTest()
    {
        //Arrange
        var registerDto = new Register
        {
            Email = "test@test.pospso",
            Password = "Passs1123.D",
        };

        var jsonStr = JsonSerializer.Serialize(registerDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync("/api/v1/identity/Account/Register/", data);

        //Assert
        response.EnsureSuccessStatusCode();

        var requestContent = await response.Content.ReadAsStringAsync();

        var resultJwt = System.Text.Json.JsonSerializer.Deserialize<JwtResponse>(
            requestContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );

        _testOutputHelper.WriteLine("Register output: ");
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(resultJwt));

        Assert.NotNull(resultJwt);
        Assert.NotNull(resultJwt!.Token);
        Assert.NotNull(resultJwt.RefreshToken);
        Assert.Equal("test@test.pospso", resultJwt.Email);
    }
    
    [Fact]
    async Task LoginApiTest()
    {
        //Arrange
        var loginDto = new Login()
        {
            Email = "admin@itcollege.ee",
            Password = "Password.1"
        };

        var jsonStr = JsonSerializer.Serialize(loginDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        //Act
        var response = await _client.PostAsync("/api/v1/identity/Account/Login/", data);

        //Assert
        response.EnsureSuccessStatusCode();

        var requestContent = await response.Content.ReadAsStringAsync();

        var resultJwt = System.Text.Json.JsonSerializer.Deserialize<JwtResponse>(
            requestContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );

        _testOutputHelper.WriteLine("Login output: ");

        _testOutputHelper.WriteLine(JsonSerializer.Serialize(resultJwt));

        Assert.NotNull(resultJwt);
    }
}