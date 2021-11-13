/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：LoginViewModel.cs
    文件功能描述：登录模型


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using System.ComponentModel.DataAnnotations;

namespace QingStack.IdentityServer.API.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.")]
        [Display(Name = "User Name", Prompt = "User Name Or Phone Number")]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Password")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
