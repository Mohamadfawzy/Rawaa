using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.Interfaces;
using System.Linq;

namespace Rawaa_Api.Services.ControlPanel
{
    public class CategoryData : IProvider<CategoryRq>
    {
        RawaaDBContext context;

        public CategoryData()
        {
            context = new RawaaDBContext();
        }

        public CategoryRq Add(CategoryRq model)
        {
            var listOfTitle = new List<CategorieTitleTranslation>();

            var category = new Category
            {
                Image = "C_" + Guid.NewGuid().ToString() + model.Image,
                CategorieTitleTranslations = listOfTitle
            };

            listOfTitle.Add(new CategorieTitleTranslation { Title = model.TitleAr, LanguageId = 1 });

            if (!string.IsNullOrEmpty(model.TitleEn))
                listOfTitle.Add(new CategorieTitleTranslation { Title = model.TitleEn, LanguageId = 2 });

            var rs = context.Categories.Add(category).Entity;

            context.SaveChanges();

            model.Id = rs.Id;
            model.Image = rs.Image;
            return model;
        }

        public CategoryRq Update(int id, CategoryRq model, string lang, bool thereImage)
        {
            var listOfTitle = new List<CategorieTitleTranslation>();
            //var category = new Category();

            var entity = context.Categories.AsNoTracking().SingleOrDefault(b => b.Id == id);

            if (entity == null)
                return null;
            // enityh is null
            if (string.IsNullOrEmpty(entity.Image) && thereImage)
                model.Image = "C_" + Guid.NewGuid().ToString() + model.Image;
            else if (!string.IsNullOrEmpty(entity.Image) && thereImage)
            {
                var newNameImage = Path.ChangeExtension(entity.Image, model.Image);
                model.Image = newNameImage;
            }



            listOfTitle.Add(new CategorieTitleTranslation { Title = model.TitleAr, CategorieId = id, LanguageId = 1 });
            listOfTitle.Add(new CategorieTitleTranslation { Title = model.TitleEn, CategorieId = id, LanguageId = 2 });


            entity.Image = model.Image;

            context.CategorieTitleTranslations.AsNoTracking().Where(t => t.CategorieId == id).ToList();
            context.UpdateRange(listOfTitle);
            context.Update(entity).Property(p => p.Image).IsModified = thereImage; //Entry(entity).CurrentValues.SetValues(category);
            context.SaveChanges();


            model = Find(id);
            return model;

        }

        public CategoryRq Delete(int id)
        {
            var entity = context.Categories.Find(id);
            if (entity == null)
                return null;
            
            List<string> productsImages = (from p in context.Products
                              where p.CategoryId == id
                              select p.Image).ToList();

            var result = context.Remove(entity);
            var category = new CategoryRq();
            category.Id = id;
            category.Image = entity.Image;
            category.ProductsImages = productsImages;
            category.ProductsCount = productsImages.Count;
            context.SaveChanges();

            return category;//new CategoryRq() { Id = result.Entity.Id, Image = entity.Image };
        }

        public CategoryRq Find(int? id)
        {
            //var rs = context.Categories.Find(id);
            var products = new List<CategoryRq>();
            products = (from c in context.Categories
                        join t in context.CategorieTitleTranslations on c.Id equals t.CategorieId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where t.CategorieId == id && t.LanguageId == 1
                        select new CategoryRq { Id = c.Id, TitleAr = t.Title, Image = c.Image }

                        into ar

                        join en in context.CategorieTitleTranslations on ar.Id equals en.CategorieId
                        where en.LanguageId == 2 && ar.Id == en.CategorieId

                        select new CategoryRq
                        {
                            Id = ar.Id,
                            TitleAr = ar.TitleAr,
                            TitleEn = en.Title,
                            Image = ar.Image,

                        }).ToList();
            return products.FirstOrDefault();
        }
        public IList<CategoryRq> List(string lang)
        {
            var products = new List<CategoryRq>();
            products = (from c in context.Categories
                        join t in context.CategorieTitleTranslations on c.Id equals t.CategorieId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where t.LanguageId == 1
                        select new CategoryRq { Id = c.Id, TitleAr = t.Title, Image = c.Image }

                        into ar

                        join en in context.CategorieTitleTranslations on ar.Id equals en.CategorieId
                        where en.LanguageId == 2


                        select new CategoryRq
                        {
                            Id = ar.Id,
                            TitleAr = ar.TitleAr,
                            TitleEn = en.Title,
                            Image = ar.Image,

                        }).ToList();
            return products;
        }

        public List<CategoryRq> Search(string searchString)
        {
            var category = (from t in context.CategorieTitleTranslations
                            where t.Title.Contains(searchString)
                            select t

                            into title

                            join c in context.Categories on title.CategorieId equals c.Id
                            select new CategoryRq
                            {
                                Title = title.Title,
                                Image = c.Image,
                                Id = c.Id
                            }).ToList();
            return category;
        }





        // ------------------------
        public IList<CategoryRq> oldList(string lang)
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

            var cat = new List<CategoryRq>();

            foreach (var p in products)
            {
                cat.Add(new CategoryRq() { Id = p.Id, Image = p.Image, TitleAr = p.Title, TitleEn = p.TitleEn });
            }

            if (products != null)
            {
                return cat;
            }
            else return null;

            return cat;
        }



    }
}
