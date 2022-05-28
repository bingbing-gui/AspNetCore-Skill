using EPPlus.FrameWork.Practice.Common;
using EPPlus.FrameWork.Practice.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPPlus.FrameWork.Practice.Service
{
    public class ExcelService : IExcelService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExcelService> _logger;
        public ExcelService(IWebHostEnvironment env, ILogger<ExcelService> logger)
        {
            _env = env;
            _logger = logger;
        }
        /// <summary>
        /// 生成Excel 文件
        /// </summary>
        /// <returns>1.文件流 2.文件名称</returns>
        public async Task<(Stream, string)> GenerateExcelFile()
        {
            var filepath = $"{_env.ContentRootPath}/format/EPPlus 测试.xlsx";
            var filename = "test file.xlsx";
            Stream returnStream;
            string key = Guid.NewGuid().ToString();
            var fileinfo = new FileInfo(filepath);
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var orders = await CreateOrders();
                using (var excelPackage = new ExcelPackage(fileinfo))
                {
                    //该对象指向不同的sheet 页
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
                    int row = 1;//默认从第一行开始
                    foreach (var order in orders)
                    {
                        worksheet.Cells[row, 1].Value = order.OrderId;
                        worksheet.Cells[row, 2].Value = order.Name;
                        worksheet.Cells[row, 3].Value = order.Price;
                        worksheet.Cells[row, 4].Value = order.Quantity;
                        worksheet.Cells[row, 5].Value = order.CreateTime;
                        row++;
                    }
                    //ExcelWorksheet worksheet2 = excelPackage.Workbook.Worksheets[2];
                    returnStream = new MemoryStream(excelPackage.GetAsByteArray());
                }
                return (returnStream, filename);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async Task<List<Order>> CreateOrders()
        {
            var orders = new List<Order>()
            {
                new Order{ OrderId=1, Name="Coffe", Price=30, Quantity=1, CreateTime=DateTimeOffset.Now },
                new Order{ OrderId=2, Name="Hamburger ", Price=40, Quantity=1, CreateTime=DateTimeOffset.Now },
                new Order{ OrderId=3, Name="Cake", Price=50, Quantity=2, CreateTime=DateTimeOffset.Now },
                new Order{ OrderId=4, Name="Milk", Price=60, Quantity=3, CreateTime=DateTimeOffset.Now }
            };
            return orders;
        }

        public async Task<Stream> ZipExcelFile()
        {
            var dic = new Dictionary<string, Stream>();
            var item= await GenerateExcelFile();
            ZipHelper zip = new ZipHelper();
            dic.Add(item.Item2, item.Item1);
            return await zip.CompressFile(dic, null);
        }
    }
}
