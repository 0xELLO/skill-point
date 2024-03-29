﻿using System;
using System.Linq;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests.WebApp;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
{
    private static bool dbInitialized = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // find DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>));

            // if found - remove
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // and new DbContext
            services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });



            // create db and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();

            try
            {
                var cat = db.GameCategory.Add(new GameCategory
                {
                    Name = {["en-GB"] = "test"},
                    Description = {["en-GB"] = "test"},
                });
                db.SaveChanges();
                db.MatchType.Add(new MatchType
                {
                    Name = "multiplayer",
                });
                db.MatchType.Add(new MatchType
                {
                    Name = "singleplayer",
                });
                db.SaveChanges();
                db.Game.Add(new Game
                {
                    GameCategoryId = cat.Entity.Id,
                    Title =
                    {
                        ["en-GB"] = "test"
                    },
                    ShortDescription =
                    {
                        ["en-GB"] = "test"
                    },
                    LongDescription =
                    {
                        ["en-GB"] =
                            "test"
                    },
                    LogoUrl = "test"

                });

            }
            catch (Exception ex)
            {

                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }

            try
            {
                using var userManager = scopedServices.GetService<UserManager<AppUser>>();
                if (dbInitialized == false)
                {
                    dbInitialized = true;
                    var users = new (string username, string password, string roles)[]
                    {
                        ("admin@itcollege.ee", "Password.1", "user,admin"),
                        ("user@itcollege.ee", "Password.1", "user"),
                        ("newuser@itcollege.ee", "Password.1", ""),
                    };

                    foreach (var userInfo in users)
                    {
                        var user = userManager!.FindByEmailAsync(userInfo.username).Result;
                        if (user == null)
                        {
                            user = new AppUser()
                            {
                                Email = userInfo.username,
                                UserName = userInfo.username,
                                EmailConfirmed = true,
                            };
                            var identityResult = userManager.CreateAsync(user, userInfo.password).Result;

                            if (!identityResult.Succeeded)
                            {
                                throw new ApplicationException("Cannot create user!");
                            }
                        }
                    
                        if (!string.IsNullOrWhiteSpace(userInfo.roles))
                        {
                            var identityResultRole = userManager.AddToRolesAsync(user,
                                userInfo.roles.Split(",").Select(r => r.Trim())
                            ).Result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }
        });
    }
}