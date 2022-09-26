using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using App.BLL.Mapper;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.DTO;
using App.DAL.EF;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Xunit.Abstractions;
using MatchType = App.Bll.DTO.MatchType;

namespace Tests.WebApp.Unit;

public class TestMatchType
{
    private readonly Mock<IAppBll> _bllMock;
    private readonly Mock<IMatchTypeRepository> _IMatchTypeRepository;
    private readonly IMatchTypeService _IMatchTypeService;
    private readonly AutoMapper.IMapper _mapper;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public TestMatchType(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _bllMock = new Mock<IAppBll>();
        _IMatchTypeRepository = new Mock<IMatchTypeRepository>();
        
        _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<App.BLL.AutoMapperConfig>(); }));

        _IMatchTypeService = new MatchTypeService(_IMatchTypeRepository.Object, new MatchTypeMapper(_mapper));
    }

    [Fact]
    public void TestGameCategory_GetAllAsync()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new List<App.DAL.DTO.MatchType>()
        {
            new()
            {
                Id = guid,
                Name = "test",
            }
        };

        var expectedBll = new List<App.Bll.DTO.MatchType>()
        {
            new()
            {
                Id = guid,
                Name = "test",
            }
        };

        _IMatchTypeRepository.Setup(x => x.GetAllAsync(true)).ReturnsAsync(expectedDal);
        //Act
        var result = _IMatchTypeService.GetAllAsync().Result.ToList();
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));
        //Assert
        Assert.Equal(expectedDal[0].Id, result[0].Id);
        Assert.Equal(expectedDal[0].Name, result[0].Name);
    }
    
    [Fact]
    public void TestGameCategory_GetAll()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new List<App.DAL.DTO.MatchType>()
        {
            new()
            {
                Id = guid,
                Name = "test",
            }
        };

        var expectedBll = new List<App.Bll.DTO.MatchType>()
        {
            new()
            {
                Id = guid,
                Name = "test",
            }
        };

        _IMatchTypeRepository.Setup(x => x.GetAll(true)).Returns(expectedDal);
        //Act
        var result = _IMatchTypeService.GetAll().ToList();
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));
        //Assert
        Assert.Equal(expectedBll[0].Id, result[0].Id);
        Assert.Equal(expectedBll[0].Name, result[0].Name);
    }

    [Fact]
    public void TestGameCategory_Add()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
                Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
                Id = guid,
                Name = "test",
            
        };

        _IMatchTypeRepository.Setup(x =>
            x.Add(It.Is<App.DAL.DTO.MatchType>(category => 
                category.Id == expectedDal.Id && category.Name == expectedDal.Name))).Returns(expectedDal);

        //Act
        var result = _IMatchTypeService.Add(expectedBll);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));
        _testOutputHelper.WriteLine(result.Name);
        //Assert
        Assert.Equal(expectedDal.Id, result.Id);
        Assert.Equal(expectedDal.Name, result.Name);
    }

    [Fact]
    public void TestGameCategory_Update()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
            Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
            Id = guid,
            Name = "test",
            
        };
        
        _IMatchTypeRepository.Setup(x =>
            x.Update(It.Is<App.DAL.DTO.MatchType>(category => 
                category.Id == expectedDal.Id && category.Name == expectedDal.Name 
                ))).Returns(expectedDal);

        //Act
        var result = _IMatchTypeService.Update(expectedBll);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.Equal(expectedDal.Id, result.Id);
        Assert.Equal(expectedDal.Name, result.Name);
    }
    
    [Fact]
    public void TestGameCategory_Remove()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
            Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
            Id = guid,
            Name = "test",
            
        };
        
        _IMatchTypeRepository.Setup(x =>
            x.Remove(It.Is<App.DAL.DTO.MatchType>(category => 
                category.Id == expectedDal.Id && category.Name == expectedDal.Name 
            ))).Returns(expectedDal);
        
        _IMatchTypeRepository.Setup(x =>
            x.Remove(It.Is<Guid>(g => g == expectedBll.Id))).Returns(expectedDal);
        
        //Act
        var result = _IMatchTypeService.Remove(expectedBll);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.Equal(expectedDal.Id, result.Id);
        Assert.Equal(expectedDal.Name, result.Name);
        
        //Act
        result = _IMatchTypeService.Remove(expectedBll.Id);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.Equal(expectedDal.Id, result.Id);
        Assert.Equal(expectedDal.Name, result.Name);
    }
    
        [Fact]
    public void TestGameCategory_RemoveAsync()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
            Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
            Id = guid,
            Name = "test",
            
        };
        
        
        
        _IMatchTypeRepository.Setup(x =>
            x.RemoveAsync(It.Is<Guid>(g => g == expectedBll.Id))).ReturnsAsync(expectedDal);
        
        //Act
        var result = _IMatchTypeService.RemoveAsync(expectedBll.Id).Result;
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.Equal(expectedDal.Id, result.Id);
        Assert.Equal(expectedDal.Name, result.Name);
    }
    
    [Fact]
    public void TestGameCategory_FirstOrDefault()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
            Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
            Id = guid,
            Name = "test",
            
        };


        _IMatchTypeRepository.Setup(x =>
            x.FirstOrDefault(It.Is<Guid>(g => g == expectedBll.Id), true)).Returns(expectedDal);

        //Act
        var result = _IMatchTypeService.FirstOrDefault(expectedBll.Id);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.Equal(expectedDal.Id, result!.Id);
        Assert.Equal(expectedDal.Name, result.Name);
    }
    
    [Fact]
    public void TestGameCategory_FirstOrDefaultAsync()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
            Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
            Id = guid,
            Name = "test",
            
        };


        _IMatchTypeRepository.Setup(x =>
            x.FirstOrDefaultAsync(It.Is<Guid>(g => g == expectedBll.Id), true)).ReturnsAsync(expectedDal);

        //Act
        var result = _IMatchTypeService.FirstOrDefaultAsync(expectedBll.Id).Result;
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.Equal(expectedDal.Id, result!.Id);
        Assert.Equal(expectedDal.Name, result.Name);
    }
    
    [Fact]
    public void TestGameCategory_Exists()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
            Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
            Id = guid,
            Name = "test",
            
        };

        _IMatchTypeRepository.Setup(x =>
            x.Exists(It.Is<Guid>(g => g == expectedBll.Id))).Returns(true);

        //Act
        var result = _IMatchTypeService.Exists(expectedBll.Id);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestGameCategory_ExistsAsync()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var expectedDal = new App.DAL.DTO.MatchType()
        {
            Id = guid,
            Name = "test",
        };

        var expectedBll = new App.Bll.DTO.MatchType()
        {
          
            Id = guid,
            Name = "test",
            
        };

        _IMatchTypeRepository.Setup(x =>
            x.ExistsAsync(It.Is<Guid>(g => g == expectedBll.Id))).ReturnsAsync(true);

        //Act
        var result = _IMatchTypeService.ExistsAsync(expectedBll.Id).Result;
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(result));

        //Assert
        Assert.True(result);
    }
    
}




