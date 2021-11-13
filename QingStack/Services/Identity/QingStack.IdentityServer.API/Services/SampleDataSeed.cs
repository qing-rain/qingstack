/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SampleDataSeed.cs
    文件功能描述：演示数据生成器


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/
using IdentityModel;
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
            await ClearClientAndUserDatas(app);
            await SeedClientDatasAsync(app);
            await SeedUserDatasAsync(app);
        }

        private static async Task ClearClientAndUserDatas(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
            await persistedGrantDbContext.Database.EnsureDeletedAsync();

            var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            await configurationDbContext.Database.EnsureDeletedAsync();

            var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await applicationDbContext.Database.EnsureDeletedAsync();
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
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach ((int UserId, string UserName, string Password, string PhoneNumber, string Email, IEnumerable<Claim> Claims) in SampleDatas.Users())
            {
                ApplicationUser createdUser = await userManager.FindByNameAsync(UserName);

                if (createdUser is null)
                {
                    createdUser = new ApplicationUser { UserName = UserName, PhoneNumber = PhoneNumber, Email = Email };
                    IdentityResult result = await userManager.CreateAsync(createdUser, Password);

                    var userRoleClaims = Claims.Where(t => t.Type == JwtClaimTypes.Role || t.Type == ClaimTypes.Role);
                    IEnumerable<Claim> userClaims = Claims.Except(userRoleClaims);

                    if (result.Succeeded)
                    {
                        await userManager.AddClaimsAsync(createdUser, userClaims);
                    }

                    var userRoleNames = userRoleClaims?.Select(urc => urc.Value);

                    userRoleNames?.ToList()?.ForEach(userRole =>
                    {
                        if (!roleManager.RoleExistsAsync(userRole).Result)
                        {
                            roleManager.CreateAsync(new ApplicationRole { Name = userRole }).Wait();
                        }
                    });

                    await userManager.AddToRolesAsync(createdUser, userRoleNames);
                }
            }
        }
    }
}
