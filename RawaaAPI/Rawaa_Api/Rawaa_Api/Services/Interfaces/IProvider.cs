
namespace Rawaa_Api.Services.Interfaces
{
    public interface IProvider<T> where T : class 
    {
        IList<T> List(string lang);
        T Find(int? id);
        T Add(T entity);
        List<T> Search(string searchString);
        T Update(int id, T entity, string lang, bool udateImage = false);
        T Delete(int id);
    }
}
