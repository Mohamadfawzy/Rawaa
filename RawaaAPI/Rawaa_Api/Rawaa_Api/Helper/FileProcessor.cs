namespace Rawaa_Api.Helper
{
    public class FileProcessor
    {
        private readonly IWebHostEnvironment webHost;
        
        // ctor
        public FileProcessor(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        public string ImageExtension(string file)
        {
            FileInfo imageInfo = new FileInfo(file);
            return imageInfo.Extension;
        }

        public async Task<string> SaveImage(IFormFile? fileUplod, string ImageName)
        {
            try
            {
                if (fileUplod.Length > 0)
                {
                    string path = webHost.WebRootPath + "\\" + "Images" + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    FileInfo imageInfo = new FileInfo(fileUplod.FileName);
                    var fullImageName = path + ImageName;
                    using (FileStream fileStream = System.IO.File.Create(fullImageName))
                    {
                        await fileUplod.CopyToAsync(fileStream);
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

        public async Task<string> UpdateImage(IFormFile? image, string ImageName)
        {
            try
            {
                if (image.Length > 0)
                {
                    string path = webHost.WebRootPath + "\\" + "Images" + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    FileInfo imageInfo = new FileInfo(image.FileName);
                    var fullImageName = path + ImageName;

                    // 
                    var nameWithoutExtention = Path.ChangeExtension(ImageName,"");
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    var s = directoryInfo.GetFiles().Where(f=>f.Name.Contains(nameWithoutExtention)).FirstOrDefault();
                    
                    if (s!= null)
                    {
                        //File.Delete(fullImageName);
                        s.Delete();
                    }

                    //var fullPath = Path.ChangeExtension(fullImageName,imageInfo.Extension);

                    using (FileStream fileStream = System.IO.File.Create(fullImageName))
                    {
                        await image.CopyToAsync(fileStream);
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
                string fullPath = webHost.WebRootPath + "\\" + "Images" + "\\" + ImageName;

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

       
    }
}
