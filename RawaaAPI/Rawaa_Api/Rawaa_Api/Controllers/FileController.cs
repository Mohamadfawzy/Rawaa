using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rawaa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public IWebHostEnvironment web;

        public FileController(IWebHostEnvironment _web)
        {
            this.web = _web;
        }

        [HttpGet]
        public IActionResult GetAllFiles()
        {
            string path = web.WebRootPath + "\\Images\\";
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            var s = directoryInfo.GetFiles();
            var list = new List<string>();
            foreach (var f in s)
            {
                list.Add(f.Name);
            }
            return Ok(list);
        }
        [HttpGet("{imageName}")]
        public IActionResult Get(string imageName)
        {
            string path = web.WebRootPath + "\\Images\\";

            FileInfo imageInfo = new FileInfo(path + imageName);
            var newImageName = path + imageName;

            var filePath = path + imageName;
            if (System.IO.File.Exists(filePath))
            {
                //byte[] b = System.IO.File.ReadAllBytes(filePath);
                //return File(b, "image/jpg");
                //return PhysicalFile(newImageName,"image/jpg");
                return PhysicalFile(newImageName, "image/" + imageInfo.Extension);
            }
            return null;
        }

        [HttpDelete]
        public IActionResult delete(string imageName)
        {
            var imags = new List<string>()
            {
                "p1.jpg",
                "p2.jpg",
                "p3.jpg",
            };
            string path = web.WebRootPath + "\\" + "Images" + "\\" + imageName;

            System.IO.File.Delete(path);

            return Ok(imags);
        }
    }
}
