using Base.Domain;

namespace App.DAL.DTO.Identity;

public class AppUser : DomainEntityId
{
    
    public ICollection<UserInMatch>? UserInMatches { get; set; }
    public ICollection<UserPlayingGameRound>? UserPlayingGameRounds { get; set; }
    public ICollection<UserGameStatistics>? UserGameStatistics { get; set; }
    public ICollection<UserRoundResult>? UserRoundResults { get; set; }
    /*
    public ICollection<Message>? Messages { get; set; }
    public ICollection<UserInChatRoom>? UserInChatRooms { get; set; }
    */
}