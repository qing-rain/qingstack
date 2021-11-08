/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IUnitOfWork.cs
    文件功能描述：工作单元


    创建标识：QingRain - 2021108
 ----------------------------------------------------------------*/
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
