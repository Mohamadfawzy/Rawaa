using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Models.Client;

namespace Rawaa_Api.Services.Client
{
    public class CategoryClientData
    {
        RawaaDBContext context;

        // ctor client 
        public CategoryClientData()
        {
            context = new RawaaDBContext();
        }


        public IList<CategoryRequestClient> List(string lang)
        {
            var products = new List<CategoryRequestClient>();
            products = (from c in context.Categories
                        join t in context.CategorieTitleTranslations on c.Id equals t.CategorieId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where l.Name == lang
                        select new CategoryRequestClient 
                        {
                            Id = c.Id,
                            Title = t.Title,
                            Image = c.Image

                        }).ToList();
            return products;
        }



    }
}
