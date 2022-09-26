using AutoMapper;
using WebApp.Mappers;

namespace WebApp;

public class WebAutoMapper
{
    private readonly AutoMapper.IMapper _mapper;

    public WebAutoMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    private GameMapper? _gameMapper;
    public GameMapper GameMapper => _gameMapper ?? new GameMapper(_mapper);
}