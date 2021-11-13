/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Certificate.cs
    文件功能描述：读取证书


    创建标识：QingRain - 20211113
 ----------------------------------------------------------------*/
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace QingStack.IdentityServer.API.Certificates
{
    public static class Certificate
    {
        /// <summary>
        /// Please note that here we are using a local certificate only for testing purposes. In a 
        /// real environment the certificate should be created and stored in a secure way, which is out
        /// of the scope of this project.
        /// </summary>
        public static X509Certificate2 Get()
        {
            using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{typeof(Certificate).Namespace}.idsrvtest.pfx");
            using MemoryStream memoryStream = new();
            stream?.CopyToAsync(memoryStream).Wait();
            return new X509Certificate2(memoryStream.ToArray(), "idsrvtest");
        }
    }
}
