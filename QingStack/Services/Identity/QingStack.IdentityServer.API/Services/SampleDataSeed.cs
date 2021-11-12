/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SampleDataSeed.cs
    文件功能描述：演示数据生成器


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QingStack.IdentityServer.API.Aggregates;
using QingStack.IdentityServer.API.EntityFrameworks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingStack.IdentityServer.API.Services
{
    public static class SampleDataSeed
    {
        public static async Task SeedAsync(IApplicationBuilder app)
        {
            await SeedClientDatasAsync(app);
            await SeedUserDatasAsync(app);
        }

        private static async Task SeedClientDatasAsync(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
            await persistedGrantDbContext.Database.MigrateAsync();

            var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            await configurationDbContext.Database.MigrateAsync();

            if (!configurationDbContext.Clients.Any())
            {
                foreach (var client in SampleDatas.Clients)
                {
                    configurationDbContext.Clients.Add(client.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            if (!configurationDbContext.IdentityResources.Any())
            {
                foreach (var resource in SampleDatas.Ids)
                {
                    configurationDbContext.IdentityResources.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            if (!configurationDbContext.ApiResources.Any())
            {
                foreach (var resource in SampleDatas.Apis)
                {
                    configurationDbContext.ApiResources.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            if (!configurationDbContext.ApiScopes.Any())
            {
                foreach (var scope in SampleDatas.ApiScopes)
                {
                    configurationDbContext.ApiScopes.Add(scope.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }
        }

        private static async Task SeedUserDatasAsync(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await applicationDbContext.Database.MigrateAsync();

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Dictionary<(string UserName, string Password), IEnumerable<Claim>> users = new()
            {
                {
                    (UserName: "alice", Password: "alice"),
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@xcode.me"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://alice.xcode.me"),
                        new Claim(JwtClaimTypes.Address, @"{ 'country': 'Germany', 'locality': 'Heidelberg'}", IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                {
                    (UserName: "bob", Password: "bob"),
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@xcode.me"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://bob.xcode.me"),
                        new Claim(JwtClaimTypes.Address, @"{ 'country': 'Germany', 'locality': 'Heidelberg'}", IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
            };

            foreach (KeyValuePair<(string UserName, string Password), IEnumerable<Claim>> user in users)
            {
                ApplicationUser createdUser = await userManager.FindByNameAsync(user.Key.UserName);

                if (createdUser is null)
                {
                    createdUser = new ApplicationUser { UserName = user.Key.UserName };
                    IdentityResult result = await userManager.CreateAsync(createdUser, user.Key.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddClaimsAsync(createdUser, user.Value);
                    }
                }
            }
        }
    }
}
