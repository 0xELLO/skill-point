using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserGameStatisticsMapper : BaseMapper<App.DAL.DTO.UserGameStatistics,App.Domain.UserGameStatistics>
{
    public UserGameStatisticsMapper(IMapper mapper) : base(mapper)
    {
    }
}