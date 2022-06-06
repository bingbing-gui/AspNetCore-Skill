// See https://aka.ms/new-console-template for more information
//文件提供程序
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCore.FileProvider
{
    class Program
    {

        static void Main(string[] args)
        {
            IFileProvider physicalFileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            var contents = physicalFileProvider.GetDirectoryContents("/");
            foreach (var item in contents)
            {
                Console.WriteLine(item.Name);
            }
            //嵌入式文件提供程序
            IFileProvider embeddedFileProvider = new EmbeddedFileProvider(typeof(Program).Assembly);
            var embeddedFile = embeddedFileProvider.GetFileInfo("Embedded.txt");
            Console.WriteLine(embeddedFile.Length);
            //组合文件提供程序
            IFileProvider compositeFileProvider = new CompositeFileProvider(physicalFileProvider, embeddedFileProvider);
            var compositeFile = compositeFileProvider.GetDirectoryContents("/");
            foreach (var item in compositeFile)
            {
                Console.WriteLine(item.Name);
            }

        }

    }
}




