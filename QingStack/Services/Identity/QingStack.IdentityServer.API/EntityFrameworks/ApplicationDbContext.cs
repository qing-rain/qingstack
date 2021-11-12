/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ApplicationDbContext.cs
    文件功能描述：认证上下文


    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QingStack.IdentityServer.API.Aggregates;

namespace QingStack.IdentityServer.API.EntityFrameworks
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
