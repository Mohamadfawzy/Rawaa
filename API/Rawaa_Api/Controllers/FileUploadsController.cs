using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ITICourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadsController : Controller
    {
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext db;


        public FileUploadsController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext _db)
        {
            _webHostEnvironment = webHostEnvironment;
            db = _db;
        }

        [HttpPost("ImageUplod{id}")]
        public async Task<string> PostImage([FromForm] ImageUplod fileUplod, int id)
        {
            try
            {
                if (fileUplod.Files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\" + "Images"+ "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo imageInfo = new FileInfo(fileUplod.Files.FileName);
                    var newImageName = id + imageInfo.Extension;
                    using (FileStream fileStream = System.IO.File.Create(path + newImageName))
                    {
                        fileUplod.Files.CopyTo(fileStream);
                        fileStream.Flush();
                        return $"image name: "+ path +  newImageName;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("GetPysicalFile/{fileName}.jpg")]
        public async Task<IActionResult> GetPysicalFile([FromRoute] string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\Images\\";

            FileInfo imageInfo = new FileInfo(path + fileName);
            var newImageName = fileName + imageInfo.Extension;

            var filePath = path + fileName + ".jpg";
            if (System.IO.File.Exists(filePath))
            {
                //byte[] b = System.IO.File.ReadAllBytes(filePath);
                //return File(b, "image/jpg");
                return PhysicalFile(filePath, "image/jpg");
            }
            return null;
        }


        
        [HttpGet("DownloadImage/{imageName}")]
        public HttpResponseMessage DownloadFile(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                string filePath = _webHostEnvironment.WebRootPath + "\\Images\\";
                string fullPath = filePath + imageName;
                if (System.IO.File.Exists(fullPath))
                {

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = new FileStream(fullPath, FileMode.Open);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = imageName;
                    fileStream.Close();
                    return response;
                }
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        //private readonly IWebHostEnvironment _webHostEnvironment;
        [HttpGet("imageLocation")]
        public IActionResult ImageLink(string imageLocation)
        {
            byte[] imageData = null;

            string filePath = _webHostEnvironment.WebRootPath + "/uploads/";
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + filePath + "/" + imageLocation;


            FileInfo fileInfo = new FileInfo(fullPath);
            long imageFileLength = fileInfo.Length;

            FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return Ok(imageData);
        }

        [HttpPost("Product")]
        public async Task<IActionResult> CreateProductAsync([FromForm] OldProduct product)
        {
            if (!ModelState.IsValid)
                return (IActionResult)Task.FromResult(product);
            //create a new Product instance.
            OldProduct newproduct = new()
            {
                Description = product.Description,
                Price = product.Price,
                Size = product.Size,
                ManufactoringDate = product.ManufactoringDate
            };
            //create a Photo list to store the upload files.
            List<Photo> photolist = new List<Photo>();
            if (product.Files.Count > 0)
            {
                foreach (var formFile in product.Files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            // Upload the file if less than 2 MB
                            if (memoryStream.Length < 2097152)
                            {
                                //based on the upload file to create Photo instance.
                                //You can also check the database, whether the image exists in the database.
                                var newphoto = new Photo()
                                {
                                    Bytes = memoryStream.ToArray(),
                                    Description = formFile.FileName,
                                    FileExtension = Path.GetExtension(formFile.FileName),
                                    Size = formFile.Length,
                                };
                                //add the photo instance to the list.
                                photolist.Add(newphoto);
                            }
                            else
                            {
                                ModelState.AddModelError("File", "The file is too large.");
                            }
                        }
                    }
                }
            }
            //assign the photos to the Product, using the navigation property.
            newproduct.Photos = photolist;
            db.Products.Add(newproduct);
            db.SaveChanges();

            return Ok();
        }

        /*
        Random rnd = new Random();
        [HttpPost("Product2")]
        public async Task<IActionResult> CreateProductAsync2([FromForm] FileUplod product)
        {
            Author newphoto = new Author();

            if (product.File.Count > 0)
            {
                foreach (var formFile in product.File)
                {
                    if (formFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            // Upload the file if less than 2 MB
                            if (memoryStream.Length < 2097152)
                            {
                                //based on the upload file to create Photo instance.
                                //You can also check the database, whether the image exists in the database.
                                newphoto = new Author()
                                {
                                    imageSource = memoryStream.ToArray(),
                                    First_Name = formFile.FileName,
                                    LastName = Path.GetExtension(formFile.FileName),
                                    Id = rnd.Next(1244)
                                };
                                //add the photo instance to the list.
                            }
                            else
                            {
                                ModelState.AddModelError("File", "The file is too large.");
                            }
                        }
                    }
                }
            }

            db.Authors.Add(newphoto);
            db.SaveChanges();
            return Ok();
        }
        */


        [HttpGet("bytImage")]
        public byte[] FileToByteArray(string imageName)
        {

            string filePath = _webHostEnvironment.WebRootPath;
            // string fullPath = AppDomain.CurrentDomain.BaseDirectory + filePath + "/" + imageName;

            byte[] fileContent = null;
            System.IO.FileStream fs = new System.IO.FileStream(filePath + "\\uploads\\" + imageName + ".jpg", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
            long byteLength = new System.IO.FileInfo(filePath + "\\uploads\\" + imageName + ".jpg").Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent;
        }
        [HttpGet("bytImageAd")]
        public Ad FileToByteArray2(string imageName)
        {
            string filePath = _webHostEnvironment.WebRootPath;
            // string fullPath = AppDomain.CurrentDomain.BaseDirectory + filePath + "/" + imageName;

            Ad fileContent = new Ad() ;
            System.IO.FileStream fs = new System.IO.FileStream(filePath + "\\uploads\\" + imageName + ".jpg", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
            long byteLength = new System.IO.FileInfo(filePath + "\\uploads\\" + imageName + ".jpg").Length;
            fileContent.Image = binaryReader.ReadBytes((Int32)byteLength);
            fs.Close();
            fs.Dispose();
            binaryReader.Close();
            return fileContent;
        }








    }
}
