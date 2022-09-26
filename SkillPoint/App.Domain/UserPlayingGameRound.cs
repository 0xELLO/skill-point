﻿using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserPlayingGameRound : DomainEntityMetaId
{
    // FK AppUser
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    // FK GameRound
    public Guid GameRoundId { get; set; }
    public GameRound? GameRound { get; set; }
}