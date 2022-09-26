using AutoMapper;
using Base.DAL;

namespace App.BLL.Mapper;

public class UserGameStatisticsMapper :  BaseMapper<App.Bll.DTO.UserGameStatistics,App.DAL.DTO.UserGameStatistics>
{
    public UserGameStatisticsMapper(IMapper mapper) : base(mapper)
    {
    }
}