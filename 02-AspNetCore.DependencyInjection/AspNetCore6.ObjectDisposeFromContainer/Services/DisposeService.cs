using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore6.ObjectDisposeFromContainer
{    
    public class Service1 : IDisposable
    {
        private bool _disposed;
        public void Write(string message)
        {
            Console.WriteLine($"********************************************************************");
            Console.WriteLine($"Service1:{message}");
            Console.WriteLine($"********************************************************************");
        }
        public void Dispose()
        {
            if (_disposed)
                return;
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine("Service1.Dispose");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            _disposed = true;
        }
    }
    public class Service2 : IDisposable
    {
        private bool _disposed;
        public void Write(string message)
        {
            Console.WriteLine($"********************************************************************");
            Console.WriteLine($"Service2:{message}");
            Console.WriteLine($"********************************************************************");
        }
        public void Dispose()
        {
            if (_disposed)
                return;
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine("Service2.Dispose");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            _disposed = true;
        }
    }
    public interface IService3
    {
        public void Write(string message);
    }
    public class Service3 : IService3, IDisposable
    {
        private bool _disposed;

        public string MyKey { get; }
        public Service3(string myKey)
        {
            MyKey = myKey;
        }
        public void Write(string message)
        {
            Console.WriteLine($"********************************************************************");
            Console.WriteLine($"Service3: {message}, MyKey = {MyKey}");
            Console.WriteLine($"********************************************************************");
        }
        public void Dispose()
        {
            if (_disposed)
                return;
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine("Service3.Dispose");
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            _disposed = true;
        }
    }
}
