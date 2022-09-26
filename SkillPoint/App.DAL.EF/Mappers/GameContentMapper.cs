using App.DAL.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GameContentMapper : BaseMapper<GameContent,App.Domain.GameContent>
{
    public GameContentMapper(IMapper mapper) : base(mapper)
    {
    }
}