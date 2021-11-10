/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ISoftDelete.cs
    文件功能描述：软删除接口


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
namespace QingStack.DeviceCenter.Domain.Entities
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
