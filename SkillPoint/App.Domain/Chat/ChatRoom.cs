using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Chat;
 
public class ChatRoom : DomainEntityMetaId
{
    // FK Match
    public Guid MatchId { get; set; }
    public Match? Match { get; set; }

    [MaxLength(64)]
    public string Connection { get; set; } = default!;
    
    public ICollection<Message>? Messages { get; set; }
    public ICollection<UserInChatRoom>? UsersInChatRoom { get; set; }
}