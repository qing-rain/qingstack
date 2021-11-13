/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：AlibabaCloudOptions.cs
    文件功能描述：阿里巴巴云配置


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/

namespace QingStack.IdentityServer.API.Infrastructure.Aliyun
{
    public class AlibabaCloudOptions
    {
        public string AccessKeyId { get; set; } = null!;

        public string AccessKeySecret { get; set; } = null!;
    }
}
