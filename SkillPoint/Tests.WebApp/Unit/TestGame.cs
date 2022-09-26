using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using App.BLL.Mapper;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.EF;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Tests.WebApp.Unit;

public class TestGame
{
    private readonly Mock<IAppBll> _bllMock;
    private readonly Mock<IGameRepository> _IGameRepository;
    private readonly IGameService _IGameService;
    private readonly AutoMapper.IMapper _mapper;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public TestGame(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _bllMock = new Mock<IAppBll>();
        _IGameRepository = new Mock<IGameRepository>();
        
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<App.BLL.AutoMapperConfig>(); }));

        _IGameService = new GameService(_IGameRepository.Object, new GameMapper(_mapper));
    }
    
    [Fact]
    public void TestGameContent_GetRandomTypingGame()
    {
        //Arrange
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();
        var expectedDal = new List<App.DAL.DTO.Game>
        {
            new ()
            {
                Id = guid2,
                GameCategoryId = guid3,
                Title = 
                {
                    ["en-GB"] = "test"
                },
                ShortDescription = "test",
                LongDescription = "test",
                LogoUrl = "test" 
            },
            new () {
                Id = guid2,
                GameCategoryId = guid3,
                Title = 
                {
                    ["en-GB"] = "test"
                },
                ShortDescription = "test",
                LongDescription = "test",
                LogoUrl = "test"
            }
        };
        var expectedBll = new List<App.Bll.DTO.Game>
        {
            new ()
            {
                Id = guid2,
                GameCategoryId = guid3,
                Title = 
                {
                    ["en-GB"] = "test"
                },
                ShortDescription = "test",
                LongDescription = "test",
                LogoUrl = "test" 
            },
            new () {
                Id = guid2,
                GameCategoryId = guid3,
                Title = 
                {
                    ["en-GB"] = "test"
                },
                ShortDescription = "test",
                LongDescription = "test",
                LogoUrl = "test"
            }
        };
        
        _IGameRepository.Setup(x =>
            x.GetAllByNameAsync("test", true)).ReturnsAsync(expectedDal);
        //Act
        var result = _IGameService.GetAllByNameAsync("test").Result.ToList();
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));
        //Assert
        Assert.Equal(expectedBll[0].Title, result[0].Title);
    }
}