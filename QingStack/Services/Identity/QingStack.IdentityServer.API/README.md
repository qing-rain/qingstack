
## Identity Server Document

https://identitymodel.readthedocs.io

https://identityserver4.readthedocs.io

https://github.com/IdentityServer/IdentityServer4.Quickstart.UI

https://github.com/domaindrivendev/Swashbuckle.AspNetCore/tree/master/test/WebSites/OAuth2Integration

https://github.com/skoruba/IdentityServer4.Admin

https://github.com/dotnet/aspnetcore/tree/master/src/Identity

## 30+ Best Login Template Bootstrap 

https://www.bootstrapdash.com/bootstrap-login-template
https://github.com/nauvalazhar/bootstrap-4-login-page
https://www.bootstrapdash.com/demo/star-admin-free/jquery/src/pages/samples/login.html
https://www.bootstrapdash.com/demo/stellar-admin-free/jquery/pages/samples/login.html

## Create DbContext Migrations

Add-Migration InitialCreate -c PersistedGrantDbContext -o Migrations/PersistedGrantMigrations -Project QingStack.IdentityServer.API -StartupProject QingStack.IdentityServer.API
Add-Migration InitialCreate -c ConfigurationDbContext -o Migrations/ConfigurationMigrations -Project QingStack.IdentityServer.API -StartupProject QingStack.IdentityServer.API
Add-Migration InitialCreate -c ApplicationDbContext -o Migrations/ApplicationMigrations -Project QingStack.IdentityServer.API -StartupProject QingStack.IdentityServer.API
dotnet ef migrations add InitialCreate -c PersistedGrantDbContext -o Migrations/PersistedGrantMigrations
dotnet ef migrations add InitialCreate -c ConfigurationDbContext -o Migrations/ConfigurationMigrations
dotnet ef migrations add InitialCreate -c ApplicationDbContext -o Migrations/ApplicationMigrations

Update-Database -Context PersistedGrantDbContext -Project QingStack.IdentityServer.API -StartupProject QingStack.IdentityServer.API
Update-Database -Context ConfigurationDbContext -Project QingStack.IdentityServer.API -StartupProject QingStack.IdentityServer.API
Update-Database -Context ApplicationDbContext -Project QingStack.IdentityServer.API -StartupProject QingStack.IdentityServer.API
dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext
dotnet ef database update -c ApplicationDbContext
## Creates SSL Certificate

makecert.exe -r -n "CN=idsrvtest" -pe -sv idsrvtest.pvk -a sha1 -len 2048 -b 11/11/2020 -e 11/11/2088 idsrvtest.cer
pvk2pfx.exe -pvk idsrvtest.pvk -spc idsrvtest.cer -pfx idsrvtest.pfx -pi idsrvtest