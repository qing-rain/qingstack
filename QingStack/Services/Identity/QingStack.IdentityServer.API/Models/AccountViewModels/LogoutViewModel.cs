/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：LogoutViewModel.cs
    文件功能描述：登出模型


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
namespace QingStack.IdentityServer.API.Models.AccountViewModels
{
    public class LogoutViewModel
    {
        /// <summary>
        /// 没有为本地属性、有是远程注销功能
        /// </summary>
        public string? LogoutId { get; set; }
    }
}
