using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.Chat;

public class UserInChatRoom : DomainEntityMetaId
{
    // FK ChatRoom
    public Guid ChatRoomId { get; set; }
    public ChatRoom? ChatRoom { get; set; }
    
    // FK AppUser
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}