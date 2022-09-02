using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rawaa.Services
{
    internal interface IRequestProvider<T>
    {
        Task<T> GetOneAsync(string uri, int id = 0, string token = "");
        Task<T> GetListAsync(int uri = 0, string token = "");
        Task<T> PostOneAsync(T data, string uri, string token = "");
        Task<T> UpdateeOneAsync(int id, string uri, string token = "");
        Task<T> DeleteOneAsync(int id, string uri, string token = "");
    }
}
