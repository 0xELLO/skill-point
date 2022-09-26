using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class GameContentMapper : BaseMapper<App.Bll.DTO.GameContent,App.DAL.DTO.GameContent>
{
    public GameContentMapper(IMapper mapper) : base(mapper)
    {
    }
}