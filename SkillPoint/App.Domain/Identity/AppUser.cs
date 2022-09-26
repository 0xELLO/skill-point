using App.Domain.Chat;
using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser
{
    public ICollection<UserInMatch>? UserInMatches { get; set; }
    public ICollection<UserPlayingGameRound>? UserPlayingGameRounds { get; set; }
    public ICollection<UserGameStatistics>? UserGameStatistics { get; set; }
    public ICollection<UserRoundResult>? UserRoundResults { get; set; }
    public ICollection<Message>? Messages { get; set; }
    public ICollection<UserInChatRoom>? UserInChatRooms { get; set; }

    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}