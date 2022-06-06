using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WatchFile
{
    public class Program
    {
        private static readonly string _fileFilter = Path.Combine("TextFiles", "*.txt");
        public static void Main(string[] args)
        {
            #region 文件监控
            Console.WriteLine($"Monitoring for changes with filter '{_fileFilter}' (Ctrl + C to quit)...");

            while (true)
            {
                MainAsync().GetAwaiter().GetResult();
            }
            #endregion
        }
        private static async Task MainAsync()
        {
            var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            IChangeToken token = fileProvider.Watch(_fileFilter);
            var tcs = new TaskCompletionSource<object>();

            token.RegisterChangeCallback(state => ((TaskCompletionSource<object>)state).TrySetResult(null), tcs);


            await tcs.Task.ConfigureAwait(false);

            Console.WriteLine("file changed");
        }
    }
}
