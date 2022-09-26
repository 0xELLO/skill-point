using System;
using Base.Domain;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.DAL.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<LangStr>(type: "jsonb", maxLength: 32, nullable: false),
                    Description = table.Column<LangStr>(type: "jsonb", maxLength: 32, nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameOverallStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AverageScoreDistribution = table.Column<string>(type: "character varying(4094)", maxLength: 4094, nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameOverallStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PreviousToken = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    PreviousExpirationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<LangStr>(type: "jsonb", maxLength: 32, nullable: false),
                    ShortDescription = table.Column<LangStr>(type: "jsonb", maxLength: 64, nullable: false),
                    LongDescription = table.Column<LangStr>(type: "jsonb", maxLength: 128, nullable: false),
                    LogoUrl = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    GameOverallStatisticsId = table.Column<Guid>(type: "uuid", nullable: true),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_GameCategory_GameCategoryId",
                        column: x => x.GameCategoryId,
                        principalTable: "GameCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_GameOverallStatistics_GameOverallStatisticsId",
                        column: x => x.GameOverallStatisticsId,
                        principalTable: "GameOverallStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchToken = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MaxPlayers = table.Column<int>(type: "integer", maxLength: 5, nullable: false),
                    OpenedToJoin = table.Column<bool>(type: "boolean", nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_MatchType_MatchTypeId",
                        column: x => x.MatchTypeId,
                        principalTable: "MatchType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(4094)", maxLength: 4094, nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameContent_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGameStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    AverageScore = table.Column<int>(type: "integer", nullable: false),
                    BestScore = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGameStatistics_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGameStatistics_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Connection = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRoom_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameInMatch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoundAmount = table.Column<int>(type: "integer", nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameInMatch_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameInMatch_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInMatch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInMatch_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInMatch_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameRound",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Opened = table.Column<bool>(type: "boolean", nullable: false),
                    GameContentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRound_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRound_GameContent_GameContentId",
                        column: x => x.GameContentId,
                        principalTable: "GameContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRound_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_ChatRoom_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInChatRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatRoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInChatRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInChatRoom_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInChatRoom_ChatRoom_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPlayingGameRound",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameRoundId = table.Column<Guid>(type: "uuid", nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlayingGameRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPlayingGameRound_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPlayingGameRound_GameRound_GameRoundId",
                        column: x => x.GameRoundId,
                        principalTable: "GameRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoundResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameRoundId = table.Column<Guid>(type: "uuid", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: false),
                    CratedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoundResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoundResult_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoundResult_GameRound_GameRoundId",
                        column: x => x.GameRoundId,
                        principalTable: "GameRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoom_MatchId",
                table: "ChatRoom",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameCategoryId",
                table: "Game",
                column: "GameCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GameOverallStatisticsId",
                table: "Game",
                column: "GameOverallStatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_GameContent_GameId",
                table: "GameContent",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInMatch_GameId",
                table: "GameInMatch",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameInMatch_MatchId",
                table: "GameInMatch",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRound_GameContentId",
                table: "GameRound",
                column: "GameContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRound_GameId",
                table: "GameRound",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRound_MatchId",
                table: "GameRound",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_MatchTypeId",
                table: "Match",
                column: "MatchTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_AppUserId",
                table: "Message",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatRoomId",
                table: "Message",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameStatistics_AppUserId",
                table: "UserGameStatistics",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGameStatistics_GameId",
                table: "UserGameStatistics",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInChatRoom_AppUserId",
                table: "UserInChatRoom",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInChatRoom_ChatRoomId",
                table: "UserInChatRoom",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInMatch_AppUserId",
                table: "UserInMatch",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInMatch_MatchId",
                table: "UserInMatch",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayingGameRound_AppUserId",
                table: "UserPlayingGameRound",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayingGameRound_GameRoundId",
                table: "UserPlayingGameRound",
                column: "GameRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoundResult_AppUserId",
                table: "UserRoundResult",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoundResult_GameRoundId",
                table: "UserRoundResult",
                column: "GameRoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GameInMatch");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserGameStatistics");

            migrationBuilder.DropTable(
                name: "UserInChatRoom");

            migrationBuilder.DropTable(
                name: "UserInMatch");

            migrationBuilder.DropTable(
                name: "UserPlayingGameRound");

            migrationBuilder.DropTable(
                name: "UserRoundResult");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ChatRoom");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GameRound");

            migrationBuilder.DropTable(
                name: "GameContent");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "MatchType");

            migrationBuilder.DropTable(
                name: "GameCategory");

            migrationBuilder.DropTable(
                name: "GameOverallStatistics");
        }
    }
}
