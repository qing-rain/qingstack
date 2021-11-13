/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ConsentInputModel.cs
    文件功能描述：授权确认模型


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using System.Collections.Generic;

namespace QingStack.IdentityServer.API.Models.ConsentViewModels
{
    public class ConsentInputModel
    {
        public string Button { get; set; } = null!;

        public IEnumerable<string> ScopesConsented { get; set; } = null!;

        public bool RememberConsent { get; set; }

        public string ReturnUrl { get; set; } = null!;

        public string? Description { get; set; }
    }
}
