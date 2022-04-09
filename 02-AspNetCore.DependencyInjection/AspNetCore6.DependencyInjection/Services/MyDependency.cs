using AspNetCore6.DependencyInjection.Interfaces;

namespace AspNetCore6.DependencyInjection.Services
{
    public class MyDependency : IMyDependency
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"MyDependency.WriteMessage Message: {message}");
        }
    }
}
