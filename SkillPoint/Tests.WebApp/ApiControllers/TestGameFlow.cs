using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using App.DAL.EF;
using App.Domain;
using App.Public.DTO;
using App.Public.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tests.WebApp.Helpers;
using WebApp.Controllers;
using Xunit;
using Xunit.Abstractions;


namespace Tests.WebApp.ApiControllers;

public class TestGameFlow : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public TestGameFlow(CustomWebApplicationFactory<Program> factory,
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
    public async Task Api_Get_Games()
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

        var uri = "/api/v1/game";

        // ACT
        var getTestResponse = await _client.GetAsync(uri);

        // ASSERT
        getTestResponse.EnsureSuccessStatusCode();

        var body = await getTestResponse.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(body);
        
        var res = JsonHelper.DeserializeWithWebDefaults<List<App.Public.DTO.GameDTO>>(body);
        
        Assert.NotNull(res);
        Assert.NotEmpty(res);
        Assert.Matches("test",res![0].Title.Trim());
    }
    
    [Fact]
    public async Task Api_Add_Match()
    {
        // Person 1
        //Arrange
        var loginDto = new Login()
        {
            Email = "admin@itcollege.ee",
            Password = "Password.1"
        };

        var jsonStr = JsonSerializer.Serialize(loginDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/v1/identity/Account/Login/", data);
        
        response.EnsureSuccessStatusCode();
        var requestContent = await response.Content.ReadAsStringAsync();

        var resultJwt = System.Text.Json.JsonSerializer.Deserialize<JwtResponse>(
            requestContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );
        
        //Arrange
        var loginDto2 = new Login()
        {
            Email = "user@itcollege.ee",
            Password = "Password.1"
        };

        var jsonStr2 = JsonSerializer.Serialize(loginDto2);
        var data2 = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        var response2 = await _client.PostAsync("/api/v1/identity/Account/Login/", data2);
        
        response2.EnsureSuccessStatusCode();
        var requestContent2 = await response.Content.ReadAsStringAsync();

        var resultJwt2 = System.Text.Json.JsonSerializer.Deserialize<JwtResponse>(
            requestContent2,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );
        
        
        // crate match

        var match = new App.Public.DTO.MatchDTO()
        {
            MaxPlayers = 5,
            OpenedToJoin = false,
            MatchType = "multiplayer",
        };

        var apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Post;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);
        apiRequest.RequestUri = new Uri(_client.BaseAddress + "api/v1/match");
        apiRequest.Content = JsonContent.Create(match);
        
        var apiResponse = await _client.SendAsync(apiRequest);
        apiResponse.EnsureSuccessStatusCode();
        var item = await apiResponse.Content.ReadAsStringAsync();
        var matchResult = System.Text.Json.JsonSerializer.Deserialize<MatchDTO>(item);

        Assert.NotNull(matchResult);
        
        
        // assert user in match
        var UserInMathc = new UserInMatch
        {
            MatchId = matchResult!.Id,
        };
        
        apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Post;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);
        apiRequest.RequestUri = new Uri(_client.BaseAddress + "api/v1/userinmatch");
        apiRequest.Content = JsonContent.Create(UserInMathc);
        
        apiResponse = await _client.SendAsync(apiRequest);
        apiResponse.EnsureSuccessStatusCode();
        item = await apiResponse.Content.ReadAsStringAsync();
        var userInMatchResult = System.Text.Json.JsonSerializer.Deserialize<App.Bll.DTO.UserInMatch>(item);

        Assert.NotNull(userInMatchResult);
        
        
        // get game
        var uri = "/api/v1/game";
        // ACT
        var getTestResponse = await _client.GetAsync(uri);
        // ASSERT
        getTestResponse.EnsureSuccessStatusCode();
        var body = await getTestResponse.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(body);
        var game = JsonHelper.DeserializeWithWebDefaults<List<App.Public.DTO.GameDTO>>(body);
        
        
        
        // assert  game in match

        var gameInMatch = new App.Bll.DTO.GameInMatch
        {
            MatchId = matchResult!.Id,
            GameId = game![0].Id,
            RoundAmount = 1
        };

        apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Post;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);
        apiRequest.RequestUri = new Uri(_client.BaseAddress + "api/v1/gameinmatch");
        apiRequest.Content = JsonContent.Create(gameInMatch);
        
        apiResponse = await _client.SendAsync(apiRequest);
        apiResponse.EnsureSuccessStatusCode();
        item = await apiResponse.Content.ReadAsStringAsync();
        var gameInMatchResult = System.Text.Json.JsonSerializer.Deserialize<App.Bll.DTO.GameInMatch>(item);

        Assert.NotNull(gameInMatchResult);
        
        
        // add gameRound
        
        var gameRound = new App.Bll.DTO.GameRound
        {
            MatchId = matchResult!.Id,
            GameId = game![0].Id,
        };

        apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Post;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);
        apiRequest.RequestUri = new Uri(_client.BaseAddress + "api/v1/gameRound");
        apiRequest.Content = JsonContent.Create(gameRound);
        
        apiResponse = await _client.SendAsync(apiRequest);
        apiResponse.EnsureSuccessStatusCode();
        item = await apiResponse.Content.ReadAsStringAsync();
        var gameRoundResult = System.Text.Json.JsonSerializer.Deserialize<App.Bll.DTO.GameRound>(item);

        Assert.NotNull(gameInMatchResult);
        
        // add result
        
        var gamrRoundResult = new App.Bll.DTO.UserRoundResult
        {
            GameRoundId = gameRoundResult!.Id,
            Result = "fadf"
        };

        apiRequest = new HttpRequestMessage();
        apiRequest.Method = HttpMethod.Post;
        apiRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        apiRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);
        apiRequest.RequestUri = new Uri(_client.BaseAddress + "api/v1/userroundresult");
        apiRequest.Content = JsonContent.Create(gamrRoundResult);
        
        apiResponse = await _client.SendAsync(apiRequest);
        apiResponse.EnsureSuccessStatusCode();
        item = await apiResponse.Content.ReadAsStringAsync();
        var userROundResukt = System.Text.Json.JsonSerializer.Deserialize<App.Bll.DTO.UserRoundResult>(item);

        Assert.NotNull(userROundResukt);
        
        
        
        
    }
    
   
}