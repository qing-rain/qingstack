/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IEmailSender.cs
    文件功能描述：邮件发送者接口


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using System.Threading.Tasks;

namespace QingStack.IdentityServer.API.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
