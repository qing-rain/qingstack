/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ScopeViewModel.cs
    文件功能描述：授权确认模型


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/

namespace QingStack.IdentityServer.API.Models.ConsentViewModels
{
    public class ScopeViewModel
    {
        public string? Value { get; set; }

        public string? DisplayName { get; set; }

        public string? Description { get; set; }

        public bool Emphasize { get; set; }

        public bool Required { get; set; }

        public bool Checked { get; set; }
    }
}
