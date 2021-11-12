/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ApplicationUser.cs
    文件功能描述：应用用户


    创建标识：QingRain - 20211112

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Identity;

namespace QingStack.IdentityServer.API.Aggregates
{
    public class ApplicationUser : IdentityUser<int>
    {
    }
}
