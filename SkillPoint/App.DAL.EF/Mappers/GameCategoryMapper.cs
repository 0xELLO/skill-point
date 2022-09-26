using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class GameCategoryMapper : BaseMapper<GameCategory,App.Domain.GameCategory>
{
    public GameCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}