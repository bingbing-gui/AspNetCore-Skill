# 第三方库集成指南（ASP.NET Core）

本页列出了本项目中使用到的优秀第三方库，并介绍它们如何与 ASP.NET Core 集成：

## 📌 Sqids

- ✅ 简要描述：用于生成简洁可逆的短 ID
- 🔗 GitHub 地址：[https://github.com/sqids/sqids-dotnet](https://github.com/sqids/sqids-dotnet)
- 📄 官方教程：[https://sqids.org/dotnet](https://sqids.org/dotnet)
- 📁 示例代码位置：`src/10-Third-Party/Sqids/`
- 📦 NuGet 包：[Sqids](https://www.nuget.org/packages/Sqids)
- 📰 相关文章：[《ASP.NET Core 中使用 Sqids 实现url短链接编码》](https://mp.weixin.qq.com/s/sqids-demo-link) <!-- 🔁 这里替换为你实际的公众号文章链接 -->
- ⚙️ 与 ASP.NET Core 的集成点：
  - 支持泛型 DI 注入：`services.AddSingleton<SqidsEncoder<int>>()`
  - 可用于替代 URL 中的自增 ID
...

