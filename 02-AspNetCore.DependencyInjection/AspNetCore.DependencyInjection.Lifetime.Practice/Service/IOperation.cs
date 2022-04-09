using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.DependencyInjection.Lifetime.Practice
{
    public interface IOperation
    {
        string OperationId { get; }
    }
    /// <summary>
    /// 瞬间
    /// </summary>
    public interface IOperationTransient : IOperation { }
    /// <summary>
    /// 范围
    /// </summary>
    public interface IOperationScoped : IOperation { }
    /// <summary>
    /// 单例
    /// </summary>
    public interface IOperationSingleton : IOperation { }
}
