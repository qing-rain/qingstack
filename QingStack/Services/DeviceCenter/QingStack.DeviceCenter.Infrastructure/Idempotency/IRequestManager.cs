/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IRequestManager.cs
    文件功能描述：请求管理器


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.Idempotency
{
    public interface IRequestManager
    {
        Task<bool> ExistAsync(string commandId);

        Task CreateRequestForCommandAsync<T>(string commandId);
    }
}
