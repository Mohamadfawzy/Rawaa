﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Rawaa_Api.Helper
{
    public class FileProcessor
    {
        private  readonly IWebHostEnvironment webHost;
        public FileProcessor(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public  async Task<string> PostImage(ImageUplod fileUplod, string guid)
        {
            try
            {
                if (fileUplod.Files.Length > 0)
                {
                    string path = webHost.WebRootPath + "\\" + "Images" + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo imageInfo = new FileInfo(fileUplod.Files.FileName);
                    var newImageName =  guid + imageInfo.Extension;
                    using (FileStream fileStream = System.IO.File.Create(path + newImageName))
                    {
                        fileUplod.Files.CopyTo(fileStream);
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
