using System;
using System.Text.Json;
using App.BLL.Mapper;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Tests.WebApp.Unit;

public class TestMatch
{
    private readonly Mock<IAppBll> _bllMock;
    private readonly Mock<IMatchRepository> _IMatchRepository;
    private readonly IMatchService _IMatchService;
    private readonly AutoMapper.IMapper _mapper;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public TestMatch(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _bllMock = new Mock<IAppBll>();
        _IMatchRepository = new Mock<IMatchRepository>();
        
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<App.BLL.AutoMapperConfig>(); }));

        _IMatchService = new MatchService(_IMatchRepository.Object, new MatchMapper(_mapper));
    }
    
    [Fact]
    public void TestMatch_GetGameByToken()
    {
        //Arrange
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();
        var token = "123";
        var expectedDal = new App.DAL.DTO.Match
        {
            Id = guid2,
            MatchTypeId = guid3,
            MatchToken = token,
            MaxPlayers = 0,
            OpenedToJoin = false,
        };
        var expectedBll = new App.Bll.DTO.Match()
        {      
            Id = guid2,
            MatchTypeId = guid3,
            MatchToken = token,
            MaxPlayers = 0,
            OpenedToJoin = false,
        };

        _IMatchRepository.Setup(x =>
            x.GetMatchByToken(token, true)).ReturnsAsync(expectedDal);
        //Act
        var result = _IMatchService.GetMatchByToken(token).Result;
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));
        //Assert
        Assert.Equal(expectedBll.Id, result!.Id);
    }
}