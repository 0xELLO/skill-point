using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class GameCategoryMapper : BaseMapper<App.Bll.DTO.GameCategory,App.DAL.DTO.GameCategory>
{
    public GameCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}