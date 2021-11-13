/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProcessConsentResult.cs
    文件功能描述：通过授权确认模型


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using IdentityServer4.Models;

namespace QingStack.IdentityServer.API.Models.ConsentViewModels
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;

        public string RedirectUri { get; set; } = null!;

        public Client? Client { get; set; }

        public bool ShowView => ViewModel != null;

        public ConsentViewModel? ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;

        public string ValidationError { get; set; } = null!;
    }
}
