/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ResourcesController.cs
    文件功能描述：资源文件控制器


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace QingStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IStringLocalizerFactory _localizerFactory;

        public ResourcesController(IStringLocalizerFactory localizerFactory)
        {
            _localizerFactory = localizerFactory;
        }

        [HttpGet]
        public string GetHelloWorld()
        {
            IStringLocalizer stringLocalizer = _localizerFactory.Create("Welcome", Assembly.GetExecutingAssembly().ToString());

            return stringLocalizer["HelloWorld"].Value;
        }
    }
}
