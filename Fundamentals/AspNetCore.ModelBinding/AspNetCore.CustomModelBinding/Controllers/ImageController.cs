using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.CustomModelBinding.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly string _targeFilePath;
        private readonly IHostEnvironment _hostEnvironment;
        public ImageController(IConfiguration configuration,IHostEnvironment hostEnvironment)
        {
            _targeFilePath = configuration["StoredFilesPath"];
            _hostEnvironment = hostEnvironment;
        }
        [HttpPost]
        public void Post([FromForm] byte[] file, string filename)
        {
            var trustFileName = Path.GetRandomFileName();
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, trustFileName+ ".jpg");

            if (System.IO.File.Exists(filePath))
            {
                return;
            }
            System.IO.File.WriteAllBytes(filePath, file);
        }
        [HttpPost("Profile")]
        public void SaveProfile([FromForm] ProfileViewModel profileViewModel)
        {
            var trustFileName = Path.GetRandomFileName();
            var filePath = Path.Combine(_targeFilePath, trustFileName,".jpg");

            if (System.IO.File.Exists(filePath))
            {
                return;
            }
            System.IO.File.WriteAllBytes(filePath, profileViewModel.File);
        }
    }
    public class ProfileViewModel
    {
        public byte[] File { get; set; }

        public string FileName { get; set; }
    }
}
