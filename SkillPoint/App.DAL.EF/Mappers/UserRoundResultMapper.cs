using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserRoundResultMapper : BaseMapper<App.DAL.DTO. UserRoundResult,App.Domain.UserRoundResult>
{
    public UserRoundResultMapper(IMapper mapper) : base(mapper)
    {
    }
}