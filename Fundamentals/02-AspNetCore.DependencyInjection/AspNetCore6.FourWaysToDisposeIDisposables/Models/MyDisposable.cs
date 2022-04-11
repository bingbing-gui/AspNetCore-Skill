namespace AspNetCore6.FourWaysToDisposeIDisposables.Models
{
    public class MyDisposable : IDisposable
    {
        public MyDisposable()
        {
            Console.WriteLine("+ {0} was created", this.GetType().Name);
        }
        public void Dispose()
        {
            Console.WriteLine("- {0} was disposed!", this.GetType().Name);
        }
    }
    public class TransientCreatedByContainer : MyDisposable { }
    public class ScopedCreatedByFactory : MyDisposable { }
    public class SingletonCreatedByContainer : MyDisposable { }
    public class SingletonAddedManually : MyDisposable { }
}
