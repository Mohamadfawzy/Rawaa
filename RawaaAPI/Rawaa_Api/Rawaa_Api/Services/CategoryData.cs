using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Services
{
    public class CategoryData : IProvider<Category>
    {
        RawaaDBContext context;
        public static IWebHostEnvironment _webHostEnvironment;
        FileProcessor fileProcessor;

        public CategoryData(IWebHostEnvironment web)
        {
            context = new RawaaDBContext();
            _webHostEnvironment = web;
            fileProcessor = new FileProcessor(_webHostEnvironment);
        }

        public Category Add(Category entity)
        {
            var listOfTitle = new List<CategorieTitleTranslation>();
            listOfTitle.Add(new CategorieTitleTranslation { Title = entity.Title, CategorieId = entity.Id, LanguageId = 1 });

            if (!string.IsNullOrEmpty(entity.TitleEn))
                listOfTitle.Add(new CategorieTitleTranslation { Title = entity.TitleEn, CategorieId = entity.Id, LanguageId = 2 });


            var category = new Category
            {
                Image = "C_" + Guid.NewGuid().ToString(),
                CategorieTitleTranslations = listOfTitle
            };

            //var title = entity.Title;
            //var titleEn = entity.TitleEn;
            //entity.Image = "C_" + Guid.NewGuid().ToString();
            context.Categories.Add(category);
            //context.SaveChanges();

            //if (!string.IsNullOrEmpty(entity.TitleEn))
            //    context.CategorieTitleTranslations.Add(new CategorieTitleTranslation { Title = titleEn, CategorieId = entity.Id, LanguageId = 2 });
            //context.CategorieTitleTranslations.Add(new CategorieTitleTranslation { Title = title, CategorieId = entity.Id, LanguageId = 1 });

            context.SaveChanges();
            category.Title = entity.Title;
            category.TitleEn = entity.TitleEn;
            return category;
        }
        public Category Update(int id, Category model, string lang, bool thereImage)
        {
            var listOfTitle = new List<CategorieTitleTranslation>();
            var category = new Category();

            var entity = context.Categories.AsNoTracking().SingleOrDefault(b => b.Id == id);
            
            // enityh is null
            if (string.IsNullOrEmpty(entity.Image) && thereImage)
                model.Image = "C_" + Guid.NewGuid().ToString();
            else
                model.Image = entity.Image;

           
            if (!thereImage)
            {
                context.Entry(entity).Property(p => p.Image).IsModified = false;
            }

            //add ar title
            if (lang == "ar" && !string.IsNullOrEmpty(model.Title))
            {
                listOfTitle.Add(new CategorieTitleTranslation { Title = model.Title, CategorieId = id, LanguageId = 1 });
            }
            // add en title
            else if (lang == "en" && !string.IsNullOrEmpty(model.TitleEn))
            {
                listOfTitle.Add(new CategorieTitleTranslation { Title = model.TitleEn, CategorieId = id, LanguageId = 2 });
            }

            category.Id = id;
            category.Image = model.Image;
            category.Title = model.Title;
            category.TitleEn = model.TitleEn;

            context.CategorieTitleTranslations.AsNoTracking().Where(t => t.CategorieId == id).ToList();
            context.UpdateRange(listOfTitle);
            context.Entry(entity).CurrentValues.SetValues(category);

            context.SaveChanges();
            return category;

        }

        public Category Delete(int id)
        {
            var entity = context.Categories.Find(id);
            var result = context.Remove(entity);
            context.SaveChanges();

            return result.Entity;
        }

        public Category Find(int? id)
        {
            return context.Categories.Find(id);
        }

        public IList<Category> List(string lang)
        {
            var products = new List<Category>();
            products = (from c in context.Categories
                        join t in context.CategorieTitleTranslations on c.Id equals t.CategorieId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where l.Name == lang
                        //orderby translat.Id
                        select new Category
                        {
                            Id = c.Id,
                            Title = t.Title,
                            Image = c.Image,
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


    }
}
