using App.DAL.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GameMapper : BaseMapper<Game,App.Domain.Game>
{
    public GameMapper(IMapper mapper) : base(mapper)
    {
    }
}