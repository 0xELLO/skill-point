using App.Domain;
using App.Domain.Chat;
using App.Domain.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MatchType = App.Domain.MatchType;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Game> Game { get; set; } = default!;
    public DbSet<GameCategory> GameCategory { get; set; } = default!;
    public DbSet<GameContent> GameContent { get; set; } = default!;
    public DbSet<GameInMatch> GameInMatch { get; set; } = default!;
    public DbSet<GameOverallStatistics> GameOverallStatistics { get; set; } = default!;
    public DbSet<GameRound> GameRound { get; set; } = default!;
    public DbSet<Match> Match { get; set; } = default!;
    public DbSet<MatchType> MatchType { get; set; } = default!;
    public DbSet<UserInMatch> UserInMatch { get; set; } = default!;
    public DbSet<UserGameStatistics> UserGameStatistics { get; set; } = default!;
    public DbSet<UserPlayingGameRound> UserPlayingGameRound { get; set; } = default!;
    public DbSet<UserRoundResult> UserRoundResult { get; set; } = default!;
    public DbSet<ChatRoom> ChatRoom { get; set; } = default!;
    public DbSet<Message> Message { get; set; } = default!;
    public DbSet<UserInChatRoom> UserInChatRoom { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

    
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            builder
                .Entity<Game>()
                .Property(e => e.Title)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<Game>()
                .Property(e => e.ShortDescription)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<Game>()
                .Property(e => e.LongDescription)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<GameCategory>()
                .Property(e => e.Name)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<GameCategory>()
                .Property(e => e.Description)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
        }

        
        // Remove cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    
    private static string SerialiseLangStr(LangStr lStr) => System.Text.Json.JsonSerializer.Serialize(lStr);

    private static LangStr DeserializeLangStr(string jsonStr) =>
        System.Text.Json.JsonSerializer.Deserialize<LangStr>(jsonStr) ?? new LangStr();


    public override int SaveChanges()
    {
        FixEntities(this);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntities(this);
        return base.SaveChangesAsync(cancellationToken);
    }

    private void FixEntities(AppDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }
}