/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IdentityProfileService.cs
    文件功能描述：Identity个性化服务 第二种方式


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using QingStack.IdentityServer.API.Aggregates;
using System.Threading.Tasks;

namespace QingStack.IdentityServer.API.Infrastructure.Tenants
{
    public class IdentityProfileService : ProfileService<ApplicationUser>
    {
        public IdentityProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory) : base(userManager, claimsFactory)
        {
        }

        public IdentityProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, ILogger<ProfileService<ApplicationUser>> logger) : base(userManager, claimsFactory, logger)
        {
        }

        protected override Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
        {
            return base.GetProfileDataAsync(context, user);
        }
    }
}
