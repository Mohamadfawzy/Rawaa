using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Rawaa_Api.Helper
{
    public class FileProcessor
    {
        private readonly IWebHostEnvironment webHost;
        public FileProcessor(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public async Task<string> SaveImage(ImageUplod? fileUplod, string ImageName)
        {
            try
            {
                if (fileUplod.Images.Length > 0)
                {
                    string path = webHost.WebRootPath + "\\" + "Images" + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    FileInfo imageInfo = new FileInfo(fileUplod.Images.FileName);
                    var fullImageName = ImageName + imageInfo.Extension;

                    using (FileStream fileStream = System.IO.File.Create(path + fullImageName))
                    {
                        await fileUplod.Images.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return fullImageName;
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
        public async Task<string> UpdateImage(ImageUplod? image, string ImageName)
        {
            try
            {
                if (image.Images.Length > 0)
                {
                    string path = webHost.WebRootPath + "\\" + "Images" + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    FileInfo imageInfo = new FileInfo(image.Images.FileName);
                    var fullImageName = ImageName + imageInfo.Extension;


                    using (FileStream fileStream = System.IO.File.Create(path + fullImageName))
                    {
                        await image.Images.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return fullImageName;
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
        public async Task<string> RemoveImage(string ImageName)
        {
            try
            {
                string fullPath = webHost.WebRootPath + "\\" + "Images" + "\\" + ImageName + ".jpg" ;

                FileInfo imageInfo = new FileInfo(fullPath);

                if (imageInfo.Exists)
                {
                    imageInfo.Delete();
                }

                return "is deleated ok";

            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> PostImage(ImageUplod fileUplod, string guid)
        {
            try
            {
                if (fileUplod.Images.Length > 0)
                {
                    string path = webHost.WebRootPath + "\\" + "Images" + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo imageInfo = new FileInfo(fileUplod.Images.FileName);
                    var newImageName = guid + imageInfo.Extension;
                    using (FileStream fileStream = System.IO.File.Create(path + newImageName))
                    {
                        fileUplod.Images.CopyTo(fileStream);
                        fileStream.Flush();
                        return newImageName;
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
    }
}
