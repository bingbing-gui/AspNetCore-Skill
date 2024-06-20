AspNetCore Skill
==============================
这个仓库主要包含了ASP.NET Core 学习资料以及代码例子，包括ASP.NET Identity、ASP.NET Core、Entity Framework Core、核心知识点，同时也包含了ASP.NET Core 第三方开源库。
# [Chapter 01](https://github.com/bingbing-gui/aspnetcore-skill/tree/master/src/Chapter01)
## ASP.NET Core Identity
ASP.NET Core Identity提供给我们一组工具包和API，它能帮助我们应用程序创建授权和认证功能，也可以用它创建账户并使用用户名和密码进行登录，同时也提供了角色和角色管理功能。ASP.NET Core Identity使用SQL Server/第三方数据库存储用户名和密码，角色和配置数据。

这系列中我们主要使用VS中自带的LocalDB作为演示，你也可以直接从官网上进行下载：[SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16)

### 1. 创建项目
理解ASP.NET Core Identity最好的方法是通过一个项目学习，我们创建一个ASP.NET Core MVC 项目，名字叫Identity，接下来，我们将配置该项目并且安装必要的包。

### 2. 配置项目
安装下列NuGet包：
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`

### 3. 配置项目
在`Program.cs`类中添加认证和授权中间件，在`app.UseRouting`后面添加如下代码：
```csharp
app.UseAuthentication();
app.UseAuthorization();
```
### 4. 设置ASP.NET Core Identity

ASP.NET Core Identity整个设置过程包括创建新的Model类、配置更改、Controller和Action支持身份验证和授权的操作。

#### 4.1 User类
User类表示应用程序中的用户，这些用户数据存储在数据库中，User类继承自`IdentityUser`类，位于命名空间`Microsoft.AspNetCore.Identity`中，在`Models`文件夹下创建`AppUser.cs`类。
```csharp
namespace Identity.Models {
    public class AppUser : IdentityUser { }
}
```
AppUser类没有包含任何方法，这是因为IdentityUser类中提供了一些用户属性像用户名，电子邮件，电话，密码hash值等
如果IdentityUser类不能满足你的要求，你可以在AppUser中添加自己定义的属性，我们会在后面介绍
IdentityUser 类定义如下常用属性：
| 名称          | 描述                                     |
| ------------- | ---------------------------------------- |
| Id            | 用户唯一ID                               |
| UserName      | 用户名称                                 |
| Email         | 用户Email                                |
| PasswordHash  | 用户密码的Hash值                         |
| PhoneNumber   | 用户电话号码                             |
| SecurityStamp | 当每次用户的数据修改时生成随机值         |

#### 4.2 创建DataBase Context

DataBase Context类继承自`IdentityDbContext<T>`类，T表示User类，在应用程序中使用`AppUser`，`IdentityDbContext`通过使用Entity Framework Core和数据库进行交互。

在`Models`文件夹下创建一个`AppIdentityDbContext`类并继承`IdentityDbContext<AppUser>`类，如下代码：
```csharp
namespace Identity.Models{
   public class AppIdentityDbContext: IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):
            base(options)
        { }
    }
}
```
#### 4.3 创建数据库字符串链接

ASP.NET Core Identity 数据库连接字符串包含数据库名，用户名，密码。通常存储在`appsettings.json`文件中，这个文件位于根目录下。
项目已经包含了这个文件，添加下面配置在你的`appsettings.json`文件中：
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=IdentityDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
连接字符串中`Server`指定SQL Server的LocalDB，`Database`指定数据库名称`IdentityDB`，你也可以起个别的名字。

`Trusted_Connection` 设置为`true`，项目通过使用Windows认证链接到数据库，因此我们不需要提供用户名和密码。

`MultipleActiveResultSets` 该特性表示允许在单个连接中执行多个批处理，使SQL语句执行更快，因此我们将它设置成`true`。

使用`AddDbContext()`方法添加`AppIdentityDbContext`类并且指定它使用SQL Server数据库，连接字符串通过配置文件中获取：

```csharp
builder.Services.AddDbContext<AppIdentityDbContext>(
    options =>options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
);
```
### 5、添加ASP.NET Core Identity服务
配置 ASP.NET Core Identity 相关服务，代码如下：

```csharp
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();
```
`AddIdentity`方法的参数类型指定`AppUser`类和`IdentityRole`类。

`AddEntityFrameworkStores`方法指定`Identity`使用`EF Core`作为存储和项目中使用`AppIdentityDbContext`作为DB Context。

`AddDefaultTokenProviders`方法添加默认`Token`提供程序，针对重置密码，电话号码和邮件变更操作以及生成双因子认证的token，这部分我们后面会介绍。

我们前面添加了`app.UseAuthentication()`方法，经过这个方法的每个HTTP请求会将用户的凭据将添加到Cookie或URL中。这使得用户和他发送的HTTP请求就会产生关联。

### 6. 使用EF Migration 命令创建Identity数据库

现在我们使用Entity Framework Core Migration 命令来创建Identity数据库（我们使用第二种方式）。

我们在程序包管理器控制台中运行Migration命令，你需要安装对应.NET Core CLI，通过下面命令可以安装：

```bash
dotnet tool install --global dotnet-ef
```
接下来我们需要进入 `.csproj` 项目文件所在的目录。我们在程序包管理器控制台中运行 `dir` 来查看当前所在目录。我们可以看到当前目录不是项目所在目录，而是 `Identity.sIn`。

项目文件位于`Identity`文件夹内，使用`cd ./Identity` 进入该文件 夹。我们再次运行dir命令，显示我们当前所在的目录是 `Identity.csproj`文件所在目录：

现在我们运行 EF Core Migration 命令

第一个命令：
`dotnet ef migrations add InitDBCommand `
上面的命令需要几秒钟完成，完成之后，我们可以在`Migrations`文件夹能看到生成的文件，文件中包含表的定义。
第二个命令：
`dotnet ef database update`
上面命令将创建数据库以及表，执行完成这个命令我们就可以看到我们表结构。

第二种方法我们可以不使用.NET Core CLI命令，微软给我提供了一个包`Microsoft.EntityFrameworkCore.Tools`。该包提供一些命令帮助我们生成数据库和表，在程序包管理控制台中运行如下两个命令:

`Add-Migration InitDBCommand --创建Migration 
Update-Database          --将Migration 更新到数据库`

### 7、ASP.NET Core Identity 数据库

我们查看一下刚才创建的数据库
注意：我们在·`Visual Studio` 菜单找到视图菜单并且打开`SQL Server` 对象资源管理器
`Identity` 数据库总共有8张表，这些表包含用户记录，角色，Claims，token和登录次数详细信息等

Identity数据库每张表的作用：

- **_EFMigrationsHistory**：包含了前面所有的Migration。

- **AspNetRoleClaims**：按照角色存储Claims。

- **AspNetRoles**：存储所有的角色。

- **AspNetUserClaims**：存储用户的Claims。

- **AspNetUserLogins**：存储用户的登录次数。

- **AspNetUserRoles**：存储用户对应的角色。

- **AspNetUsers**：存储所有用户。

- **AspNetUserTokens**：存储外部认证的token。

## [Chapter 02]()
## [Chapter 03]()
## [Chapter 04]()
## [Chapter 05]()
## [Chapter 06]()
## [Chapter 07]()
## [Chapter 08]()
## [Chapter 09]()
## [Chapter 10]()
## [Chapter 11]()
## [Chapter 12]()
## [Chapter 13]()
## [Chapter 14]()
## [Chapter 15]()
## [Chapter 16]()
## [Chapter 17]()
## [Chapter 18]()
## [Chapter 19]()
## [Chapter 20]()
## [Chapter 21]()
## [Chapter 22]()
| 目录                                                                                                                       |描述   |
|:--------------------------------------------------------------------------------------------- |:------------------------------------------------------------------------------------------- |
| Fundamentals                                                                                  | ASP.NET Core 基础与高阶技能全攻略                                                             |
| AspNetCore.Identity                                                                           | 主要涵盖了ASP.NET Core Identity组件中所有功能                                                 |
| EntityFrameworkCore| 主要涵盖了Entity FrameworkCore基本使用，实体增删改查，Fluent API的使用 |
| Third-Party.Library| 常用的ASP.NET Core第三方的库 |
## Fundamentals目录
| 项目名称 | 项目讲解地址 |
|:--------------------------------------------------------------------------------------------- |:--------------------------------------------------------------------------------------------|
| AspNetCore.Configuration/AspNetCore.Configuration                                             |[ASP.NET Core 配置系列一](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486815&idx=1&sn=1a4f471b2a8431d771c11d511e8efc5c&chksm=9f005275a877db63515a5304c166b561d1ac476d7d6980c2c26e192d60cdafecd1ba322196c1&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
|AspNetCore.Configuration/AspNetCore.Configuration |[ASP.NET Core 配置系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486833&idx=1&sn=a4de2c6202d559e9968f7170ede99cea&chksm=9f00525ba877db4df4a50a50590103760fd2e4603969b2b2ecfb7a54b414bfba05eecefe1af0&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Configuration/AspNetCore.Configuration |[ASP.NET Core 配置系列三](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486845&idx=1&sn=a9b153b5cff58c5732d51d439bcc5952&chksm=9f005257a877db41dc77a468a9f203deea091969ada466ff4b2730f6066fbaa094484f0d3a8d&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Configuration/AspNetCore.Configuration |[ASP.NET Core 配置系列四](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486847&idx=1&sn=81c1ba7182a6aab6c8958e211b5a9dcf&chksm=9f005255a877db43953e2215a262faedc31c7c9d9083950429a88e5f6a912d7f817a87ab4717&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Configuration/AspNetCore.Configuration |[ASP.NET Core 配置系列五](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486858&idx=1&sn=e722b863483710384138d6e885b7c95e&chksm=9f0052a0a877dbb62fd8057492b092de70c3516fb0a7b5135cfdd5d7e4b26c91af36930c243d&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Options/AspNetCore.OptionsPattern |[ASP.NET Core中Options模式](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487805&idx=1&sn=5792825947dd874cc53d188c060848c0&chksm=9f004e17a877c701a325fc9b4444a9beb95c11b4c14f967b94be302e8db56fff20ca41484e33&token=1468182562&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.DependencyInjection/AspNetCore.DependencyInjection |[ASP.NET Core 依赖注入系列一](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486869&idx=1&sn=26ff00ea2ee847d23329958268cf9c66&chksm=9f0052bfa877dba97d4f16d813080159a645162d8c42b501a2ae71e721578d5f7c069a71ea0d&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.DependencyInjection/AspNetCore.DependencyInjection |[ASP.NET Core 依赖注入系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486889&idx=1&sn=f37abf5047811527b8fbe74dabea45c8&chksm=9f005283a877db95d4a81909348c2c2b4a4ee9095b5524d93ad5b4dd35545c7e9a202a57fd59&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.DependencyInjection/AspNetCore.DependencyInjection |[ASP.NET Core 依赖注入系列三](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486896&idx=1&sn=b91b23179f4343ba4baa15c5f8ba2d99&chksm=9f00529aa877db8c8c9803bd7c6fa8913b012a422c312b4d08959e2f2c6e6c29368782d649cd&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Controllers/AspNetCore.Controllers |[ASP.NET Core Controllers](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486901&idx=1&sn=fff8d045f2f5b06dcca07251f67cc034&chksm=9f00529fa877db8906cfaf50bfc1335bff05fff1eeae61d5c02582cd499d36b588187bf0c75d&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Action/AspNetCore.Action|[ASP.NET Core Actions](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486916&idx=1&sn=f1ce92a76d20abb7888eca0b39bbbfe7&chksm=9f0052eea877dbf878b21b25219f4d5ddb82cfc3fff811dfed7c5144ad45b872cb1c1168460f&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Views/AspNetCore.Views |[ASP.NET Core Views系列一](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486961&idx=1&sn=af2c5824f8e56ec6c99e3a77a6a03d0f&chksm=9f0052dba877dbcd2aa41bc181bca8cfeccbb70b98d2b53ee494bc85b6762fa731c322b26c53&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Views/AspNetCore.Views |[ASP.NET Core Views系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486966&idx=1&sn=f790d2784b4c2fcd7e53fda17f851401&chksm=9f0052dca877dbca29e604f786d6eaf90fa498b130cb05e1b29763cf8ba61ec3b37fa1ea3ae2&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Views/AspNetCore.Views |[ASP.NET Core Views系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486966&idx=1&sn=f790d2784b4c2fcd7e53fda17f851401&chksm=9f0052dca877dbca29e604f786d6eaf90fa498b130cb05e1b29763cf8ba61ec3b37fa1ea3ae2&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Route/AspNetCore.URLRouting |[ASP.NET Core 路由](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486984&idx=1&sn=999b344fe347e79bb93b298dc9e43217&chksm=9f005122a877d834057dcf752d1909f04a96cb06862fe486baea23026bcfbd72875c0cb37dca&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Route/AspNetCore.RouteConstraint |[ASP.NET Core 路由约束](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486990&idx=1&sn=feb21fd9aeb98d0e163520516d32ebd2&chksm=9f005124a877d8320c8a1f32d019c94a9c7ed57b8b3b1b21707bd83c8c6f93074f7e46187cc6&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Route/AspNetCore.AttributeRoute |[ASP.NET Core Attribute 路由](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487003&idx=1&sn=6205c6245822eb612ff31da7b2d02046&chksm=9f005131a877d82776c4948e0aefa2858c7f7dfeb46b274d5e8ee5f6e6fe65e4354679873621&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Route/AspNetCore.RouteLinks |[ASP.NET Core 路由生成外部链接](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487004&idx=1&sn=5b187126022538c22df4493ecbad82fc&chksm=9f005136a877d8208b660272e137566718e83b1d3a82225cbadad1abe16dd1dccbf2b1412c0e&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Route/AspNetCore.Areas |[ASP.NET Core Areas](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487005&idx=1&sn=479514da1cade0b523591ea324160a15&chksm=9f005137a877d821dc58f128ab8bed9c5a59345d3a7eff81344e4ed874c8fb11ea95a9951e38&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Route/AspNetCore.Areas |[ASP.NET Core Areas](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487005&idx=1&sn=479514da1cade0b523591ea324160a15&chksm=9f005137a877d821dc58f128ab8bed9c5a59345d3a7eff81344e4ed874c8fb11ea95a9951e38&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.TagHelpers/AspNetCore.TagHelpers |[Asp.Net Core Tag Helpers 入门](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487026&idx=1&sn=f384a4d9aa77e3ce488b613b4616e228&chksm=9f005118a877d80ebd88c159b3852cbeae1c9fcfc14b414c7d2aac5a973205994e077dd7b62b&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.TagHelpers/AspNetCore.BuiltInTagHelpers |[ASP.NET Core 内置的Tag Helpers](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487064&idx=1&sn=3882bec53987f5e52e5877607f4cef0d&chksm=9f005172a877d864d3b53e83648e84b57da89ba4584a4a8cd88c7d4e1448ec20cea1f4a6c510&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.TagHelpers/AspNetCore.CustomTagHelpers |[ASP.NET Core 自定义Tag Helpers](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487076&idx=1&sn=2c1a4b79f13d7192f58bb75f4cb5cd30&chksm=9f00514ea877d8582a17815e942f4fe6c13857e87dcf43694dff961b68b34b179f2c1499552d&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.ModelBinding |[ASP.NET Core模型绑定 - 基础篇](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487093&idx=1&sn=29067d940aa2e9567848de63d454bc4f&chksm=9f00515fa877d849815b3112fd3ba73917a12fa62ea3f229374f3d48d1791af2960252906023&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.ModelBinding/AspNetCore.ModelBinding.Advanced |[ASP.NET Core模型绑定 - 高级篇](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487130&idx=1&sn=aebf769dd1cd2ce2e4975539d037100b&chksm=9f0051b0a877d8a6b47ee847627a121a79a95f0fcf4071678d7fe510899b8f6946db70c9ea5d&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.ModelValidation/AspNetCore.ModelValidation |[ASP.NET Core 模型验证系列一](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487169&idx=1&sn=811199ee67ba99e1edd38f2ca343182f&chksm=9f0051eba877d8fddc91cc2ce5f8eb2a82bb6b82bd87867e239434d1f7cf9d6ceb692fa5c598&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.ModelValidation/AspNetCore.ModelValidation |[ASP.NET Core 模型验证系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487171&idx=1&sn=8ab5881423081dce5d5c1df3f6040181&chksm=9f0051e9a877d8ff4f6feba723e79b76ce35e1dfbd011b1585331d4ebfecb2eeb70e84568ee5&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Filters/AspNetCore.Filters |[ASP.NET Core 过滤器](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487185&idx=1&sn=14f52c9b455558855918483d4e1188bb&chksm=9f0051fba877d8ed34cb8b03cb1c54098e0fe6c7fa502f0cd8b9eda480530dca0cf902d352cd&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Filters/AspNetCore.Filters |[ASP.NET Core 过滤器高级篇](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487200&idx=1&sn=4ef08760d010c34dbfd96b485f0e6888&chksm=9f0051caa877d8dcbeefd5491948cf00ee341ba416fccd5ac4722b229303bba722aa8925bfa2&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.GlobalizationLocalization/AspNetCore.GlobalizationLocalization |[ASP.NET Core 全球化和本地化系列一](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487256&idx=1&sn=2d94e80754c7d9fbee7c406a27128f7e&chksm=9f005032a877d9247e81f5af8f3d8b3028612edd6fe5eeaccf30da27bec1e527f168385a0623&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.GlobalizationLocalization/AspNetCore.GlobalLocalResFiles |[ASP.NET Core 全球化和本地化系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487271&idx=1&sn=0bdf2957d886b16c91b8e7e173346a45&chksm=9f00500da877d91b4fe30a1f52f11dd790472dd4def8957d5421c4b10d38bc506471ea7ab72d&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.GlobalizationLocalization/AspNetCore.GlobalLocalPO |[在ASP.NET Core 使用PO文件指定本地化](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487314&idx=1&sn=cf5796d612435b36a014d1ee7ce81e62&chksm=9f005078a877d96e38d28036566a92a155857269829bbcff635d75ddd958aa3f5a9a669e3ebd&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
|AspNetCore.Security/AspNetCore.CORS |[ASP.NET Core 启用CORS](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487319&idx=1&sn=b3f2ee161acaef7223099c5286bd98f9&chksm=9f00507da877d96b77e5bc7ddeb7a1f5117928da5f5be140fe2e8d95f03ea77725e85735726a&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Security/AspNetCore.Cookie|[ASP.NET Core Cookie 认证](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487335&idx=1&sn=9a2b6e1dbdc3a9d34edf22632aaceb04&chksm=9f00504da877d95b13d9eec3b9a01cf45d956cba4b91749b74b5355b29ea7a68c7ecd177851a&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Security/AspNetCore.XSRF|[ASP.NET Core XSRF/CSRF攻击](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487341&idx=1&sn=1d8d46df360a53cc3b8c76e3eda405de&chksm=9f005047a877d951317336e88d478ca58ebdcc82583edc3711894ad586ca0fe1bc3dbb066f15&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Security/AspNetCore.OpenRedirectAttacks|[ASP.NET Core 开放重定向攻击](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487345&idx=1&sn=b4b700289a2aff5b12603efab8a937ce&chksm=9f00505ba877d94d3d2eec522cc638e771b0c576741c10b9ba9c28e367e2e626d578835c8cab&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Security/AspNetCore.XSS|[ASP.NET Core XSS攻击](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487450&idx=1&sn=701a5d35e3d2e51b0f9dea374de77765&chksm=9f0050f0a877d9e692c6c5b7d5e983448f5d46925520de5132a17223325bb750e170bd487482&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.APIControllers/AspNetCore.APIControllers |[ASP.NET Core API Controllers 系列一](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487467&idx=1&sn=28d1602053ce15f754af8cd94032758c&chksm=9f0050c1a877d9d77c37edd6862b3f7af444063329e6b3c07e113e775cff7cbdfdf92aff9070&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.APIControllers/AspNetCore.APIControllers |[ASP.NET Core API Controllers 系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487550&idx=1&sn=553f2aaa7303167cd0d96fe274ab45cf&chksm=9f004f14a877c6021612f55564bed68e8f730fd63f8589f7b278ad014757f06dfbab53fd126e&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.APIControllers/AspNetCore.APIController.ActionReturnTypes |[ASP.NET Core API 返回类型](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487695&idx=1&sn=6fd98c58f0c7063a72eaeb3a1ef650fe&chksm=9f004fe5a877c6f343410008390be97d7a67dd2db265415304b0c6571fa2d394914d7bb077bc&token=1193472&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.APIControllers/AspNetCore.FormatResponseOutputData |[ASP.NET Core 指定Action的响应类型](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487766&idx=1&sn=3df7d17a1a603702aae77ae1233aa9a1&chksm=9f004e3ca877c72af99503d682478b2dae6b079603953ac764d571adfbcf591d885f3907c98e&token=1193472&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.API.Auth/AspNetCore.API.BasicAuthentication |[ASP.NET Core API Basic认证](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487777&idx=1&sn=f64b4e7663dba23b241027e9ad777b97&chksm=9f004e0ba877c71d5e11b92a3c93dc81032f9c475c171de3c273d9cfb5e3f0fdbf2d0fb896ab&token=1468182562&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.API.Auth/AspNetCore.API.JWT.Authentication |[ASP.NET Core API JWT认证](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487790&idx=1&sn=faf38bd3b0c64db5eb83b6ae6d0dfc28&chksm=9f004e04a877c712ea159eeaa902cbc1bf086a92e0f6fc30b017b689907716d816fba73cad51&token=1468182562&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.API.Auth/AspNetCore.API.JWT.Authentication |[ASP.NET Core API 刷新 access token](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487803&idx=1&sn=669242339e308d701def8073776e5ddc&chksm=9f004e11a877c7077bfe4aef8bd3641f8cf8308c5d91cdea7f34dbe975e0e97fd50e71c81ee8&token=1468182562&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.HttpRequest/AspNetCore.HttpClient |[ASP.NET Core HttpClient正确使用方法之系列一](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487588&idx=1&sn=4b621a443e862a4f82a319d5409c86b1&chksm=9f004f4ea877c658490f3678cf2ad9aa7bae24810328d57bf18039920f5111b93e8d9935fe86&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.HttpRequest/AspNetCore.HttpClientWithHttpVerb |[ASP.NET Core HttpClient使用http动词系列二](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487594&idx=1&sn=25307ab8b4fe0dd1590975bf77eb3aad&chksm=9f004f40a877c656aadd1e666ee6a347bcebeb57ae37b3a8b6bee2d9555940db07b67e5aab2e&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.HttpRequest/AspNetCore.HttpClientHander |[ASP.NET Core HttpClient组件拓展系列三](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487648&idx=1&sn=bb71d7ebec5ba3d6d09cf15062859d3f&chksm=9f004f8aa877c69ccd6ab7cf845ab7e4e9f86e393df6157a5a465158e6e5477cc58c31c0c240&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| ASP.NET Core HttpClient的实现原理系列四 |[ASP.NET Core HttpClient的实现原理系列四](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487649&idx=1&sn=1ae4da30419e6f71c70b8773597a27bb&chksm=9f004f8ba877c69dd91a433b54a08809388e00b431fe4dc683d5a3e13fd6bfd59c8352162cc5&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Swagger/AspNetCore.Swashbuckle |[ASP.NET Core 使用Swagger/OpenAPI文档化API](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487665&idx=1&sn=bb6836e776d7bbc36e547703429a2fb6&chksm=9f004f9ba877c68db2e472d8578e48001363e13aa1f20a3bf29b8732641f4613e2fb9a7e4593&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Swagger/AspNetCore.Swashbuckle |[ASP.NET Core 中使用Swashbuckle](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487666&idx=1&sn=71d91f7cbe59b809c7c7e7e054747985&chksm=9f004f98a877c68e42058c0f283c03ee45a6f303c65041bf182c8ee98239a75503039c0bc1fc&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |
| AspNetCore.Swagger/AspNetCore.NSwag |[ASP.NET Core 使用NSwag](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247487669&idx=1&sn=dd4569527a3a840c8a508936a64d235b&chksm=9f004f9fa877c689de806c882211353676f7e619bc0c532d35abc0c2ed0145dcef363b5c6ffb&token=2146175377&lang=zh_CN&scene=21#wechat_redirect) |

## AspNetCore.Identity

## Third-Party.Library
## EntityFrameworkCore目录
| 项目名称 | 描述 |
|:--------------------------------------------------------------------------------------------- |:--------------------------------------------------------------------------------------------|
| Entity FrameworkCore介绍  |  [Entity FrameworkCore介绍](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486352&idx=1&sn=7c6b71ba2d48c6a8572d4777154c0f06&chksm=9f0054baa877ddacd9054e90745ee5db786d35fc09e082a5ce5ee2f0125dd0860bab3493d753&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| Entity Framework Core安装  |  [Entity Framework Core安装](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486360&idx=1&sn=aab93026b7506e9e8c1e19d9d099ed24&chksm=9f0054b2a877dda464ddf13c6129e3fa028778336ae08313e508c5fd7befce96125df7c76902&token=1170622278&lang=zh_CN#rd)|
| Entity Framework Core数据库优先 |  [Entity Framework Core数据库优先](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486484&idx=1&sn=e0911921eac2c1c8f535829c137f35a3&chksm=9f00533ea877da280c08908716e802cd67f34fdcc5e55112a87f66f419437836c78b4e07bccc&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreDbContext |  [Entity Framework Core DbContext](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486561&idx=1&sn=3638a0ea6a034e1b1021a6a9e25b1fba&chksm=9f00534ba877da5d80c0884378079c2e1226c79fc13dafdd0145ca3a74901f1f008cf93054d8&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreCodeFirst |  [Entity Framework Core 代码优先](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486562&idx=1&sn=240686fb2d56093677a120816354dbec&chksm=9f005348a877da5eba68ea35875761088c31de6b0ef3a2082e39dfe925a6ede194e94ed6f765&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreMigration |  [Entity Framework Core-Migrations](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486566&idx=1&sn=526492ab605d4a57048eb91c75c2c65a&chksm=9f00534ca877da5a196cc1b3ff2fbc5f947baa5eb0b21f655dfd5aeb8d3e9fa86919d6150f67&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreInsertRecords |  [Entity Framework Core 插入数据](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486636&idx=1&sn=1ed995d089324fc520d5e0f120e8f05e&chksm=9f005386a877da90c3c7a3c9818ae3d2f029b1e4fc473319af6f6ca58dc50b2db54289323905&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreReadRecords |  [Entity Framework Core 读取数据](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486663&idx=1&sn=8ccb4afc723bd79752a8ffe608c6e03c&chksm=9f0053eda877dafb81a94456c6519550345412f6f840ad5978715d0a072a69ae5a234e7069ed&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreUpdateRecords |  [Entity Framework Core 更新数据](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486674&idx=1&sn=691160617db74f82bbd31ddda31a9333&chksm=9f0053f8a877daeed0713d3571442fcc65fe9577d355a27a3650033baf9c3246b8f55009ac6a&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreUpdateRecords |  [Entity Framework Core 删除数据](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486676&idx=1&sn=7a0d10ab748af23dc1bc1ecb671d7194&chksm=9f0053fea877dae88694419c6f83a8dc6ca9a51fe8f567ed751f52a567d390b823ef88b8e3fd&token=1170622278&lang=zh_CN#rd)|
| EFCoreConventions |  [Entity Framework Core 约定](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486677&idx=1&sn=4e82e2c40bc45603e8588086489bf19c&chksm=9f0053ffa877dae90feafe62f9e3e5fccb85c564a3ebaa1914eb3bf8207fb44c703525361666&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreConfiguration |  [Entity Framework Core 配置](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486678&idx=1&sn=c3fa83be65d505dde5dd9431e68cd57b&chksm=9f0053fca877daea1de89f77137a4b69abd0904246bf5eac8841b2adc789fcd9333f9d8c3581&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| Entity Framework Core-Fluent API |  [Entity Framework Core-Fluent API](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486679&idx=1&sn=3314f5325411b0c5ba8a3ef81aa15922&chksm=9f0053fda877daeb6c21a92ec3bcc319eb0eb45f69814dc1d7081a821ef26bcfdf5b190e30fa&token=1170622278&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreFluentAPIOneToOne | [Entity Framework Core-使用Fluent API配置一对一关系](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486695&idx=1&sn=47c3631d10360417b63f606c918e8679&chksm=9f0053cda877dadb2f95dbbb9ee8d9ae2c10e83e01e697cdbd1a8cd12fa8d6aee558821c6a83&token=304031109&lang=zh_CN#rd)|
| EFCoreFluentAPIOneToMany | [Entity Framework Core-使用Fluent API配置一对多关系](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486695&idx=1&sn=47c3631d10360417b63f606c918e8679&chksm=9f0053cda877dadb2f95dbbb9ee8d9ae2c10e83e01e697cdbd1a8cd12fa8d6aee558821c6a83&token=304031109&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreFluentAPIManyToMany | [Entity Framework Core-使用Fluent API配置多对多关系](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486697&idx=1&sn=b96286d4a852a865e731d67c4df25fab&chksm=9f0053c3a877dad53f37e173109dd2291412e085e1b959949e95dbc159d49e94baa709ee180f&token=323179610&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreExecuteRawSql | [Entity Framework Core-使用FromSqlRaw() 执行原生SQL查询](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486699&idx=1&sn=48713b8379f12e180270a7e8c2c100df&chksm=9f0053c1a877dad7fdd7a50fd97946d0fa74368f93d35eba0b25339d177968bffc254d95a40d&token=323179610&lang=zh_CN&scene=21#wechat_redirect)|
| EFCoreExecuteStoredProcedures | [ntity Framework Core执行存储过程](https://mp.weixin.qq.com/s?__biz=MzA3NDM1MzIyMQ==&mid=2247486707&idx=1&sn=fb77e56489b8140a9a66d5d8c8018c29&chksm=9f0053d9a877dacfdb794538d7fc28f43c74b75bd5ddcfc13e91717054eebddcfd254c4351e2&token=323179610&lang=zh_CN&scene=21#wechat_redirect)|




