/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ApplicationUser.cs
    文件功能描述：应用用户

    创建标识：QingRain - 20211112

    创建标识：QingRain - 20211114
    修改描述：增加租户ID
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Identity;
using System;

namespace QingStack.IdentityServer.API.Aggregates
{
    public class ApplicationUser : IdentityUser<int>
    {
        public Guid? TenantId { get; set; }
    }
}
