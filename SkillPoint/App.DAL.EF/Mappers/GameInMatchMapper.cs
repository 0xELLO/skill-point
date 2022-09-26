using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GameInMatchMapper : BaseMapper<GameInMatch,App.Domain.GameInMatch>
{
    public GameInMatchMapper(IMapper mapper) : base(mapper)
    {
    }
}