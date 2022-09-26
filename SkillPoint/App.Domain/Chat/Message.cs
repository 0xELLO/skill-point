using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.Chat;

public class Message : DomainEntityMetaId
{
    // FK ChatRoom
    public Guid ChatRoomId { get; set; }
    public ChatRoom? ChatRoom { get; set; }
    
    // FK AppUser
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(64)]
    public string Text { get; set; } = default!;
}