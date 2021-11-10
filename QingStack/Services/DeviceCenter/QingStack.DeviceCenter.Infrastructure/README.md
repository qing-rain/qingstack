# 基础设施层 数据存储介质


## Add Tool
install-package Microsoft.EntityFrameworkCore.SqlServer

install-package Microsoft.EntityFrameworkCore.Tools

## Create DbContext Migrations
Add-Migration InitialCreate -Context DeviceCenterDbContext -Project QingStack.DeviceCenter.Infrastructure -StartupProject QingStack.DeviceCenter.Infrastructure

Update-Database -Context DeviceCenterDbContext -Project QingStack.DeviceCenter.Infrastructure -StartupProject QingStack.DeviceCenter.Infrastructure