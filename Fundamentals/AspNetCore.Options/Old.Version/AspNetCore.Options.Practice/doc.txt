
1.支持单利模式读取配置
2.支持快照
3.支持配置变更通知
4.支持运行时动态修改选项值

设计原则
接口分离原则(ISP) , 我们的类不应该依赖它不适用的配置
关注点分离，不同组件、服务、类之间的配置不应该相互依赖和耦合

为我们服务器设计XXXOptions

使用IOptions<XXXOptions>、IOptionsSnapshot<XXXOptions>、IOptionsMonitor<XXXOptions> 作为服务器构造函数的参数


作用域模式采用IOptionsSnapshot
单例模式采用IOptionsMonitor
IPostConfigureOptions<TOptions>
