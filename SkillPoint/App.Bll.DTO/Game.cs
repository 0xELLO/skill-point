using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Bll.DTO;

public class Game : DomainEntityId
{
    // FK GameCatergory
    public Guid GameCategoryId { get; set; }
    public GameCategory? GameCategory { get; set; }
    
    [MaxLength(32)]
    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Game), Name = nameof(Title))]
    public LangStr Title { get; set; } = new();
    
    [MaxLength(64)]
    [Column(TypeName = "jsonb")]
    public LangStr ShortDescription { get; set; } =  new();
    
    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    public LangStr LongDescription { get; set; } =  new();

    [MaxLength(128)]
    [Column(TypeName = "jsonb")]
    public string LogoUrl { get; set; } = default!;
    /*
    public ICollection<GameInMatch>? GameInMatches { get; set; }

    public ICollection<GameContent>? GameContents { get; set; }

    public ICollection<GameRound>? GameRounds { get; set; }

    public ICollection<UserGameStatistics>? UsersGameStatistics { get; set; }
    
    // 1 - 1/0
    public GameOverallStatistics? GameOverallStatistics { get; set; }
    */
}