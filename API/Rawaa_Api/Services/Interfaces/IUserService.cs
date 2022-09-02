using Rawaa_Api.Helper;

namespace Rawaa_Api.Services.Interfaces
{
    public interface IUserService<T>
    {
        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(T model);
        void Update(int id, T model);
        void Delete(int id);
    }
}
