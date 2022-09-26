using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MatchType = System.IO.MatchType;

namespace WebApp;

public static class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment enc, IConfiguration configuration)
    {
        
        using var serviceScope = app
            .ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("problem in services no db context");
        }
        
        if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") return;

        // TODO - check data base state
        // can't connect
        // can't connect wrong user 
        // can connect - but no database
        // can connect - but no user

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }

        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }

        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            if (userManager == null || roleManager == null)
            {
                throw new NullReferenceException("user manager or role manager is null!!");
            }

            var roles = new string[]
            {
                "admin",
                "user"
            };

            foreach (var roleInfo in roles)
            {
                var role = roleManager.FindByNameAsync(roleInfo).Result;
                if (role == null)
                {
                    var identityResult = roleManager.CreateAsync(new AppRole()
                    {
                        Name = roleInfo,
                    }).Result;

                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed");
                    }
                }
            }

            var users = new (string username, string password, string roles)[]
            {
                ("admin@itcollege.ee", "Password.1", "user,admin"),
                ("user@itcollege.ee", "Password.1", "user"),
                ("newuser@itcollege.ee", "Password.1", ""),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.username).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.username,
                        UserName = userInfo.username,
                        EmailConfirmed = true
                    };
                    var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Cannot create user!");
                    }
                }

                if (!string.IsNullOrWhiteSpace(userInfo.roles))
                {
                    var identityResultRole = userManager.AddToRolesAsync(user, userInfo.roles.Split(",")
                            .Select(r => r.Trim()))
                        .Result;
                }
            }

        }

        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            var gameCategoryTyping = new GameCategory{
                Name =
                {
                    ["en-GB"] = "Typing"
                },
                Description =                 {
                    ["en-GB"] = "Typing game"
                }
            };
            
            var gameCategoryMemory = new GameCategory{
                Name =
                {
                    ["en-GB"] = "Memory"
                },
                Description =  {
                    ["en-GB"] = "Typing game"
                }
            };

            var gameCategoryReaction = new GameCategory
            {
                Name =
                {
                    ["en-GB"] = "Reaction "
                },
                Description =
                {
                    ["en-GB"] = "Reaction game"
                }
            };

            context.GameCategory.Add(gameCategoryTyping);
            context.GameCategory.Add(gameCategoryMemory);
            context.GameCategory.Add(gameCategoryReaction);

            context.SaveChanges();

            var gameTyping = new Game
            {
                GameCategoryId = gameCategoryTyping.Id,
                Title =
                {
                    ["en-GB"] = "Typing"
                },
                ShortDescription =
                {
                    ["en-GB"] = "How many words per minute can you type?"
                },
                LongDescription =
                {
                    ["en-GB"] =
                        "The faster you type, the faster you communicate with others. See how fast you type! Easy and fun way to test and improve your typing speed"
                },
                LogoUrl = "keyboard"
            };
            
            var gameMemory = new Game
            {
                GameCategoryId = gameCategoryMemory.Id,
                Title =
                {
                    ["en-GB"] = "Number memory"
                },
                ShortDescription =
                {
                    ["en-GB"] = "Remember the longest number you can"
                },
                LongDescription =
                {
                    ["en-GB"] =
                        "The average person can only remember 7 digit numbers reliably, can you do better?"
                },
                LogoUrl = "memory"
            };
            
            var gameReaction = new Game
            {
                GameCategoryId = gameCategoryReaction.Id,
                Title =
                {
                    ["en-GB"] = "Reaction"
                },
                ShortDescription =
                {
                    ["en-GB"] = "Test your visual reflexes"
                },
                LongDescription =
                {
                    ["en-GB"] =
                        "Click fast!!!"
                },
                LogoUrl = "reaction"
            };
            
            context.Game.Add(gameTyping);
            context.Game.Add(gameMemory);
            context.Game.Add(gameReaction);
            context.SaveChanges();

            var gameContents = new List<GameContent>
            {
                new()
                {
                    GameId = gameTyping.Id,
                    Content = "I've come to believe that each of us has a personal calling that's as unique as a fingerprint - and that the best way to succeed is to discover what you love and then find a way to offer it to others in the form of service, working hard, and also allowing the energy of the universe to lead you.",
                },
                new()
                {
                    GameId = gameTyping.Id,
                    Content = "Everything you've learned in school as 'obvious' becomes less and less obvious as you begin to study the universe. For example, there are no solids in the universe. There's not even a suggestion of a solid. There are no absolute continuums. There are no surfaces. There are no straight lines.",
                },
                             new()
                                {
                                    GameId = gameTyping.Id,
                                    Content = "We all experience many freakish and unexpected events - you have to be open to suffering a little. The philosopher Schopenhauer talked about how out of the randomness, there is an apparent intention in the fate of an individual that can be glimpsed later on. When you are an old guy, you can look back, and maybe this rambling life has some through-line. Others can see it better sometimes. But when you glimpse it yourself, you see it more clearly than anyone.",
                                },
                                             new()
                                                {
                                                    GameId = gameTyping.Id,
                                                    Content = "Quantum mechanics is certainly imposing. But an inner voice tells me that it is not yet the real thing. The theory says a lot, but does not really bring us any closer to the secret of the ‘old one.’ I, at any rate, am convinced that He does not throw dice.",
                                                }
            };

            context.GameContent.AddRange(gameContents);
            context.SaveChanges();

            var matchType = new App.Domain.MatchType
            {
                Name = "singleplayer",
            };
            
            var matchType2 = new App.Domain.MatchType
            {
                Name = "multiplayer",
            };
            context.MatchType.Add(matchType);
            context.MatchType.Add(matchType2);
            context.SaveChanges();
            
        }
    }
}