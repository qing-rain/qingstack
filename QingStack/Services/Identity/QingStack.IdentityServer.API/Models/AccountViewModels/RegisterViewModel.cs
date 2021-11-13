/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：RegisterViewModel.cs
    文件功能描述：账号注册模型


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QingStack.IdentityServer.API.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "User Name", Prompt = "User Name")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.")]
        [RegularExpression("^[a-z]+$", ErrorMessage = "The {0} must be lowercase letter.")]
        //远程验证
        [Remote(action: "VerifyUserName", controller: "Account")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required."), Phone]
        [Display(Name = "Phone Number", Prompt = "Phone Number")]
        [RegularExpression(@"^1\d{10}$", ErrorMessage = "The {0} must be 11 digits.")]
        [Remote(action: "VerifyPhoneNumber", controller: "Account")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "Confirmed Code", Prompt = "Confirmed Code")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "The {0} must be 6 digits.")]
        public string ConfirmedCode { get; set; } = null!;
    }
}
