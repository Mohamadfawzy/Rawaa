using Rawaa_Api.Models;
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Services
{
    public class CategoryData : IProvider<Category>
    {
        RawaaDBContext context;
        public static IWebHostEnvironment _webHostEnvironment;

        public CategoryData(IWebHostEnvironment web)
        {
            context = new RawaaDBContext();
            _webHostEnvironment = web;
        }

        public Category Add(Category entity)
        {
            //entity.NameAr = "ss";
            entity.Image = "C_" + Guid.NewGuid().ToString().Substring(0,12);
            
            context.Categories.Add(entity);
            context.SaveChanges();

            context.CategorieTitleTranslations.Add(new CategorieTitleTranslation { TitleAr = entity.Title, CategoryId = entity.Id });
            context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Category Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IList<Category> List()
        {

            var products = new List<Category>();
            products = (from p in context.Categories
                        join translat in context.CategorieTitleTranslations on p.Id equals translat.CategoryId
                        orderby translat.Id
                        select new Category
                        {
                            Id = p.Id,
                            Title = translat.TitleAr,
                            Image = p.Image,
                        }).ToList();

            if (products != null)
            {
                return products;
            }
            else return null;


            return context.Categories.ToList<Category>();
        }

        public List<Category> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
