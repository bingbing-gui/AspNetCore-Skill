using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AspNetCore.ChangeToToken.Utilities.Utilities;

namespace AspNetCore.ChangeToToken.Service
{
    public class FileService
    {
        private readonly IMemoryCache _cache;

        private readonly IFileProvider _fileProvider;

        private List<string> _token = new List<string>();

        public FileService(IMemoryCache cache, IWebHostEnvironment webHostEnvironment)
        {
            _cache = cache;
            _fileProvider = webHostEnvironment.ContentRootFileProvider;
        }
        public async Task<string> GetFileContents(string filename)
        {
            var filePath = _fileProvider.GetFileInfo(filename).PhysicalPath;
            string fileContent;

            if (_cache.TryGetValue(filePath, out fileContent))
            {
                return fileContent;
            }
            fileContent = await GetFileContent(filePath);

            if (fileContent != null)
            {
                //监控文件名- 
                var changeToken = _fileProvider.Watch(filename);

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .AddExpirationToken(changeToken);
                _cache.Set(filePath, fileContent, memoryCacheEntryOptions);
                return fileContent;
            }
            return string.Empty;
        }
    }
}
