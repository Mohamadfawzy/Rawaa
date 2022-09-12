
namespace Rawaa_Api.Services.Interfaces
{
    public interface IProvider<T> where T : class 
    {
        IList<T> List(string lang);
        T Find(int? id);
        T Add(T model);
        List<T> Search(string searchString);
        T Update(int id, T model, string lang, bool udateImage = false);
        T Delete(int id);
    }
}
