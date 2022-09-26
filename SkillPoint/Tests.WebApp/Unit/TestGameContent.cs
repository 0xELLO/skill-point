using System;
using System.Collections.Generic;
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

public class TestGameContent
{
    private readonly Mock<IAppBll> _bllMock;
    private readonly Mock<IGameContentRepository> _IGameContentRepository;
    private readonly IGameContentService _IGameContentService;
    private readonly AutoMapper.IMapper _mapper;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public TestGameContent(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _bllMock = new Mock<IAppBll>();
        _IGameContentRepository = new Mock<IGameContentRepository>();
        
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<App.BLL.AutoMapperConfig>(); }));

        _IGameContentService = new GameContentService(_IGameContentRepository.Object, new GameContentMapper(_mapper));
    }
    
    [Fact]
    public void TestGameContent_GetRandomTypingGame()
    {
        //Arrange
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();
        var expectedDal = new GameContent
        {
            GameId = guid3,
            Content = "content, content",
        };
        var expectedBll = new App.Bll.DTO.GameContent()
        {      GameId = guid3,
            Content = "content, content",
            
        };
        _IGameContentRepository.Setup(x =>
            x.GetRandomTypingGame( true)).ReturnsAsync(expectedDal);
        //Act
        var result = _IGameContentService.GetRandomTypingGame().Result;
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));
        //Assert
        Assert.Equal(expectedBll.Id, result.Id);
    }
}