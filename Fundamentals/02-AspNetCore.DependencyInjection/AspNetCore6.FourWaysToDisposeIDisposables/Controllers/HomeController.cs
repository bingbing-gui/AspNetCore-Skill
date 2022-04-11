using AspNetCore6.FourWaysToDisposeIDisposables.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace AspNetCore6.FourWaysToDisposeIDisposables.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly IDisposable _disposable;

        public HomeController(
        TransientCreatedByContainer transient,
        ScopedCreatedByFactory scoped,
        SingletonCreatedByContainer createdByContainer,
        SingletonAddedManually manually,
        ILogger<HomeController> logger)
        {
            //_disposable = new MyDisposable();
            _logger = logger;
        }
        /// <summary>
        /// 1.可以通过FromService获取服务
        /// 2.HttpContext.RequestServices 获取服务
        /// </summary>
        /// <param name="singletonCreatedByContainer"></param>
        /// <returns></returns>
        public IActionResult Index([FromServices]SingletonCreatedByContainer singletonCreatedByContainer)
        {
            //HttpContext.Response.RegisterForDispose(_disposable);
            return View();
        }

        public IActionResult Privacy()
        {
            using (var disposal = new MyDisposable())
            {

            }
            MyDisposable myObject=null;
            try
            {
                myObject = new MyDisposable();
            }
            finally
            {
                myObject?.Dispose();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}