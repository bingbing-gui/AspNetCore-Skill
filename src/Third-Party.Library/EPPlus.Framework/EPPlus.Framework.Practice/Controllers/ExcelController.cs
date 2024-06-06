using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPlus.FrameWork.Practice.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EPPlus.FrameWork.Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _excelService;
        private readonly ILogger<ExcelController> _logger;
        public ExcelController(IExcelService excelService, ILogger<ExcelController> logger)
        {
            _excelService = excelService;
            _logger = logger;
        }
        /// <summary>
        /// 下载Excel 文件
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 200)]
        [HttpGet("download")]
        public async Task<IActionResult> GenerateExcelFormat()
        {
            try
            {
                var ret = await _excelService.GenerateExcelFile();
                HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
                return File(ret.Item1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ret.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                Response.StatusCode = 500;
                return new JsonResult(new { ex.Message });
            }
        }
        /// <summary>
        /// 压缩并下载(如果压缩文件需要引用SharpZipLib)
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 200)]
        [HttpGet("zip/download")]
        public async Task<IActionResult> ZipExcelFormat()
        {
            try
            {
                var ret = await _excelService.ZipExcelFile();
                HttpContext.Response.ContentType = "application/zip";
                return File(ret, "application/zip", "zip file");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                Response.StatusCode = 500;
                return new JsonResult(new { ex.Message });
            }
        }


    }
}
