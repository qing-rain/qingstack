/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ErrorViewModel.cs
    文件功能描述：错误消息


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/

namespace QingStack.IdentityServer.API.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } = null!;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public IdentityServer4.Models.ErrorMessage? Error { get; set; }
    }
}
