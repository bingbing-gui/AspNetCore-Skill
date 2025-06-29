| 特性 | Active Directory Domain Services | Microsoft Entra ID |
|------|-------|---------------------|
| 服务类型 | 目录服务 | 身份解决方案 |
| 部署结构 | 基于 X.500 的层次结构 | 扁平结构 |
| 管理单元 | 支持组织单位（OU）和组策略对象（GPO） | 不支持 OU 和 GPO |
| 网络协议 | 使用 LDAP 查询与管理 | 使用 REST API，通过 HTTP/HTTPS 访问 |
| 身份验证协议 | 使用 Kerberos 协议 | 使用 SAML、WS-Federation、OpenID Connect（身份验证）和 OAuth（授权） |
| 多租户支持 | 以单租户为主，支持域信任 | 原生多租户架构 |
| 资源定位方式 | 使用 DNS 定位资源（如域控制器） | 不依赖 DNS 定位 |
| 计算机对象 | 包含计算机对象，表示加入域的计算机 | 不包含传统计算机对象 |
| 联合身份服务 | 通过域信任实现委派管理 | 包含联合身份服务，可与如 Facebook 等第三方联合 |
| 可否部署在 Azure 虚拟机 | 可以部署于 Azure VM，用于增强本地 AD DS | 不依赖 VM，直接由 Microsoft 云托管 |
| 与 Entra ID 的关系 | 与 Entra ID 无集成 | 是 Entra 产品家族的核心组件 |
| 存储要求 | 不应使用 C 盘；需配置数据磁盘存储数据库、日志和 sysvol，磁盘缓存设为 None | 无需手动配置磁盘 |
