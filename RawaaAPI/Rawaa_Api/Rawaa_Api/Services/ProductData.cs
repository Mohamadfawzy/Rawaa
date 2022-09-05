using Microsoft.AspNetCore.Hosting;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Services
{
    public class ProductData : IProvider<Product>
    {
        RawaaDBContext context;
        Product defaultModel = new Product()
        {
            Id = 1,
            Title = "",
            Image = "",
            SmallSizePrice = 1,
            MediumSizePrice = 1,
            BigSizePrice = 1,
            DiscountValue = 1,
            Calories = 11,
            HasTaste = 1,
            CategoryId = 1
        };
        public static IWebHostEnvironment _webHostEnvironment;
        public ProductData(IWebHostEnvironment web)
        {
            context = new RawaaDBContext();
            _webHostEnvironment = web;
        }
        public Product Add(Product entity)
        {
            entity.Image = "P_"+Guid.NewGuid().ToString().Substring(0, 12); ;
            var result = context.Products.Add(entity);
            context.SaveChanges();
            var title = context.ProductTitleTranslations.Add(new ProductTitleTranslation { TitleAr = entity.Title, ProductId = entity.Id });
            context.SaveChanges();
            return entity;
        }

        public Product Find(int? id)
        {
            var product = new Product();
            if (id is int)
            {
                product = (from p in context.Products
                           join translat in context.ProductTitleTranslations on p.Id equals translat.ProductId
                           orderby translat.Id
                           where p.Id == id
                           select new Product
                           {
                               Id = p.Id,
                               Title = translat.TitleAr,
                               Image = p.Image,
                               SmallSizePrice = p.SmallSizePrice,
                               MediumSizePrice = p.MediumSizePrice,
                               BigSizePrice = p.BigSizePrice,
                               DiscountValue = p.DiscountValue,
                               Calories = p.Calories,
                               HasTaste = p.HasTaste,
                               CategoryId = p.CategoryId
                           }).FirstOrDefault();
            }
            if (product != null)
            {
                return product;
            }
            else return null;
        }

        public IList<Product> List()
        {

            var products = new List<Product>();
            products = (from p in context.Products
                       join translat in context.ProductTitleTranslations on p.Id equals translat.ProductId
                       orderby translat.Id
                       select new Product
                       {
                           Id = p.Id,
                           Title = translat.TitleAr,
                           Image = p.Image,
                           SmallSizePrice = p.SmallSizePrice,
                           MediumSizePrice = p.MediumSizePrice,
                           BigSizePrice = p.BigSizePrice,
                           DiscountValue = p.DiscountValue,
                           Calories = p.Calories,
                           HasTaste = p.HasTaste,
                           CategoryId = p.CategoryId
                       }).ToList();

            if (products != null)
            {
                return products;
            }
            else return null;


            return context.Products.ToList<Product>();

        }

        public List<Product> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Product entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string UploudImage(ImageUplod source)
        {
            string path = _webHostEnvironment.WebRootPath + "\\" + "Images" + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileInfo imageInfo = new FileInfo(source.Files.FileName);
            var newImageName = "name1" + imageInfo.Extension;
            using (FileStream fileStream = System.IO.File.Create(path + newImageName))
            {
                source.Files.CopyTo(fileStream);
                fileStream.Flush();
                return $"image name: " + path + newImageName;
            }
        }
    }
}
