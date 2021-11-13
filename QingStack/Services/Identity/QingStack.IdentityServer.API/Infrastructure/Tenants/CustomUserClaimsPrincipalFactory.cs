/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomUserClaimsPrincipalFactory.cs
    文件功能描述：自定义用户声明工厂 第一种方式


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QingStack.IdentityServer.API.Aggregates;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingStack.IdentityServer.API.Infrastructure.Tenants
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            //保存基类声明
            ClaimsIdentity claimsIdentity = await base.GenerateClaimsAsync(user);

            if (user.TenantId.HasValue)
            {
                //租户不为空，添加租户声明
                claimsIdentity.AddClaim(new Claim(TenantClaimTypes.TenantId, user.TenantId.Value.ToString()));
            }

            return claimsIdentity;
        }
    }
}
