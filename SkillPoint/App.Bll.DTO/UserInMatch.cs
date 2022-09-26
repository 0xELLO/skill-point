﻿using App.Bll.DTO.Identity;
using Base.Domain;

namespace App.Bll.DTO;

public class UserInMatch : DomainEntityMetaId
{
    // Fk AppUser
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    // FK Match
    public Guid MatchId { get; set; }
    public Match? Match { get; set; }
}