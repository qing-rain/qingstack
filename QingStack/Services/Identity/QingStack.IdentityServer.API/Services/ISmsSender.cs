/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ISmsSender.cs
    文件功能描述：短信发送者接口


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using System.Threading.Tasks;

namespace QingStack.IdentityServer.API.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
