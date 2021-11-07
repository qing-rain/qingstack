using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Wechat.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
#pragma warning disable IDE0052 // 删除未读的私有成员
        private readonly ILogger<ErrorModel> _logger;
#pragma warning restore IDE0052 // 删除未读的私有成员

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }
        [AllowNull]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
