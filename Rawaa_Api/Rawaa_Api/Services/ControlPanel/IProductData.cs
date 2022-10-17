using Rawaa_Api.Models.ControlPanel;

namespace Rawaa_Api.Services.ControlPanel
{
    public interface IProductData
    {
        ProductRequest Add(ProductRequest model, string imageExtension);
        ProductRequest Delete(int id);
        bool DeleteRange(int[] range);
        ProductRequest Find(int? id);
        IList<ProductRequest> List(string lang);
        List<ProductRequest> Search(string searchString);
        ProductRequest Update(int id, ProductRequest model, string lang, bool isThereImage);
        ProductRequest? UpdateImage(int id, string extension);
    }
}