using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPPlus.FrameWork.Practice.Service
{
    public interface IExcelService
    {
        Task<(Stream, string)> GenerateExcelFile();

        Task<Stream> ZipExcelFile();
    }
}
