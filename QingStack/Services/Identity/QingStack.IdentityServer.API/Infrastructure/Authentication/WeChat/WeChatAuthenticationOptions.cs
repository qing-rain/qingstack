/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using static Microsoft.AspNetCore.Authentication.WeChat.WeChatAuthenticationConstants;

namespace Microsoft.AspNetCore.Authentication.WeChat
{
    /// <summary>
    /// Defines a set of options used by <see cref="WeChatAuthenticationHandler"/>.
    /// </summary>
    public class WeChatAuthenticationOptions : OAuthOptions
    {
        public WeChatAuthenticationOptions()
        {
            ClaimsIssuer = WeChatAuthenticationDefaults.Issuer;
            CallbackPath = new PathString(WeChatAuthenticationDefaults.CallbackPath);

            AuthorizationEndpoint = WeChatAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = WeChatAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = WeChatAuthenticationDefaults.UserInformationEndpoint;

            Scope.Add("snsapi_login");
            Scope.Add("snsapi_userinfo");

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "unionid");
            ClaimActions.MapJsonKey(ClaimTypes.Name, "nickname");
            ClaimActions.MapJsonKey(ClaimTypes.Gender, "sex");
            ClaimActions.MapJsonKey(ClaimTypes.Country, "country");
            ClaimActions.MapJsonKey(Claims.OpenId, "openid");
            ClaimActions.MapJsonKey(Claims.Province, "province");
            ClaimActions.MapJsonKey(Claims.City, "city");
            ClaimActions.MapJsonKey(Claims.HeadImgUrl, "headimgurl");
            ClaimActions.MapCustomJson(Claims.Privilege, user =>
            {
                if (!user.TryGetProperty("privilege", out var value) || value.ValueKind != System.Text.Json.JsonValueKind.Array)
                {
                    return null;
                }

                return string.Join(",", value.EnumerateArray().Select(element => element.GetString()));
            });
        }
    }
}
