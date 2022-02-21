/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：AccountController.cs
    文件功能描述：账户控制器


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using QingStack.IdentityServer.API.Aggregates;
using QingStack.IdentityServer.API.EntityFrameworks;
using QingStack.IdentityServer.API.Models.AccountViewModels;
using QingStack.IdentityServer.API.Services;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingStack.IdentityServer.API.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IStringLocalizerFactory _localizerFactory;
        private readonly IDistributedCache _distributedCache;
        private readonly ISmsSender _smsSender;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory, IIdentityServerInteractionService interactionService, ApplicationDbContext dbContext, IStringLocalizerFactory localizerFactory, IDistributedCache distributedCache, ISmsSender smsSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _interactionService = interactionService;
            _dbContext = dbContext;
            _localizerFactory = localizerFactory;
            _distributedCache = distributedCache;
            _smsSender = smsSender;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AutoLogin(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var authorizationRequest = await _interactionService.GetAuthorizationContextAsync(returnUrl);

            LoginViewModel? loginViewModel = null;

            if (authorizationRequest?.Client?.ClientId is not null)
            {
                loginViewModel = new LoginViewModel { UserName = authorizationRequest.LoginHint };
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginViewModel);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        //防伪标记
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                ApplicationUser? user = _dbContext.Users.FirstOrDefault(u => u.UserName == model.UserName || u.PhoneNumber == model.UserName);

                if (user is null)
                {
                    ModelState.AddModelError(nameof(user.UserName), "Invalid login user name.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user?.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    if (_interactionService.IsValidReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToLocal(returnUrl!);
                }
                //双重验证
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        /// <summary>
        /// 跳转本地登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private IActionResult RedirectToLocal(string? returnUrl)
        {
            //是否本网站地址
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendCode([System.ComponentModel.DataAnnotations.Phone] string phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Generate the token and send it
            //分布式缓存存储验证码
            string code = _distributedCache.GetString(phoneNumber);

            if (string.IsNullOrWhiteSpace(code))
            {
                Random random = new((int)DateTime.Now.Ticks);
                code = random.Next(100000, 999999).ToString();
            }

            await _distributedCache.SetStringAsync(phoneNumber, code, new DistributedCacheEntryOptions
            {
                //设置缓存有效期
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            await _smsSender.SendSmsAsync(phoneNumber, code);

            return Ok();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //防伪验证
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                //校验数据库是否存在
                if (_dbContext.Users.Any(u => u.UserName == model.UserName || u.PhoneNumber == model.PhoneNumber))
                {
                    ModelState.AddModelError(string.Empty, "User name or phone number is already in use.");
                    return View(model);
                }
                //校验验证码
                if (string.IsNullOrWhiteSpace(model.ConfirmedCode) || await _distributedCache.GetStringAsync(model.PhoneNumber) != model.ConfirmedCode)
                {
                    ModelState.AddModelError(nameof(model.ConfirmedCode), "Invalid confirmed code.");
                    return View(model);
                }
                //创建用户
                var user = new ApplicationUser { UserName = model.UserName, PhoneNumber = model.PhoneNumber };
                var result = await _userManager.CreateAsync(user, model.Password);

                //生成token
                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);

                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();

                if (result.Succeeded)
                {
                    //自动登录
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);

                        // Update any authentication tokens as well
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="logoutId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            //没有登录 直接退出
            if (User.Identity?.IsAuthenticated == false)
            {
                // if the user is not authenticated, then just show logged out page
                return await Logout(new LogoutViewModel { LogoutId = logoutId });
            }

            //登出确认
            //Test for Xamarin. 
            LogoutRequest logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            if (logoutRequest?.ShowSignoutPrompt == false)
            {
                //it's safe to automatically sign-out
                return await Logout(new LogoutViewModel { LogoutId = logoutId });
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            LogoutViewModel logoutViewModel = new() { LogoutId = logoutId };

            return View(logoutViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutViewModel model)
        {
            string? idp = User?.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

            if (idp is not null && idp != IdentityServerConstants.LocalIdentityProvider)
            {
                if (model.LogoutId is null)
                {
                    // if there's no current logout context, we need to create one
                    // this captures necessary info from the current logged in user
                    // before we signout and redirect away to the external IdP for signout
                    model.LogoutId = await _interactionService.CreateLogoutContextAsync();
                }

                string url = Url.Action("Logout", new { model.LogoutId });

                //登出后返回到应用所在URL上
                if (await HttpContext.GetSchemeSupportsSignOutAsync(idp))
                {
                    // hack: try/catch to handle social providers that throw
                    await HttpContext.SignOutAsync(idp, new AuthenticationProperties { RedirectUri = url });
                }
            }

            // delete authentication cookie
            await HttpContext.SignOutAsync();

            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // get context information (client name, post logout redirect URI and iframe for federated signout)
            LogoutRequest logoutRequest = await _interactionService.GetLogoutContextAsync(model.LogoutId);

            return logoutRequest?.PostLogoutRedirectUri is null ? RedirectToLocal(null) : Redirect(logoutRequest?.PostLogoutRedirectUri);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);

                if (user is null)
                {
                    ModelState.AddModelError(string.Empty, "Phone number does not exist.");
                    return View(model);
                }

                if (string.IsNullOrWhiteSpace(model.ConfirmedCode) || await _distributedCache.GetStringAsync(model.PhoneNumber) != model.ConfirmedCode)
                {
                    ModelState.AddModelError(nameof(model.ConfirmedCode), "Invalid confirmed code.");
                    return View(model);
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var identityResult = await _userManager.ResetPasswordAsync(user, code, model.Password);

                if (identityResult.Succeeded)
                {
                    return RedirectToLocal(null);
                }

                identityResult.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e.Description));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        /// <summary>
        /// 引导第三方登录
        /// </summary>
        /// <param name="provider">第三方类型，界面传入名称和依赖注入名称要一致</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string? returnUrl = "~/")
        {
            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && !_interactionService.IsValidReturnUrl(returnUrl))
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return url");
            }

            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), new { ReturnUrl = returnUrl });//第三方登录后回调
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info is null)
            {
                return RedirectToAction(nameof(Login));
            }

            ApplicationUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user is not null)
            {
                await _signInManager.SignInAsync(user, true);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded && returnUrl is not null && user is not null)
            {
                // Update any authentication tokens if login succeeded
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);

                return RedirectToLocal(returnUrl);
            }

            return View(viewName: nameof(Register));
        }

        //Identity错误转换为模型错误信息
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>
        /// AJAX远程验证手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST"), AllowAnonymous]
        public IActionResult VerifyPhoneNumber(string phoneNumber)
        {
            var _localizer = _localizerFactory.Create(typeof(RegisterViewModel));

            if (_dbContext.Users.Any(u => u.PhoneNumber == phoneNumber))
            {
                return Json(_localizer["Phone number is already in use."].Value);
            }

            return Json(true);
        }
        /// <summary>
        /// AJAX远程验证用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST"), AllowAnonymous]
        public IActionResult VerifyUserName(string userName)
        {
            var _localizer = _localizerFactory.Create(typeof(RegisterViewModel));

            if (_dbContext.Users.Any(u => u.UserName == userName))
            {
                return Json(_localizer["User name is already in use."].Value);
            }

            return Json(true);
        }
    }
}
