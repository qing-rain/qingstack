/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ConsentViewModel.cs
    文件功能描述：授权确认模型


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using System.Collections.Generic;

namespace QingStack.IdentityServer.API.Models.ConsentViewModels
{
    public class ConsentViewModel : ConsentInputModel
    {
        public string? ClientName { get; set; }

        public string? ClientUrl { get; set; }

        public string? ClientLogoUrl { get; set; }

        public bool AllowRememberConsent { get; set; }

        public IEnumerable<ScopeViewModel>? IdentityScopes { get; set; }

        public IEnumerable<ScopeViewModel>? ApiScopes { get; set; }
    }
}
