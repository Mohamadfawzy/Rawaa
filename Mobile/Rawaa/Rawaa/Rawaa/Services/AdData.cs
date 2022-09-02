using Newtonsoft.Json;
using Rawaa.Models;
using Rawaa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdData))]
namespace Rawaa.Services
{
    public class AdData : IDataStore<AdsM>
    {
        readonly List<AdsM> items;
        HttpClientHandler httpClientHandler = new HttpClientHandler();

        HttpClient clint;
        public AdData()
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            clint = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri("https://192.168.1.101:7128"),
                Timeout = new TimeSpan(0, 0, 10)
            };
        }
        public async Task<AdsM> GetItemAsync(int id)
        {
            var json = await clint.GetStringAsync("api/FileUploads/bytImage?imageName=1");
            var result = JsonConvert.DeserializeObject<byte[]>(json);

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<AdsM>> GetItemsAsync(bool forceRefresh = false)
        {
            var json = await clint.GetStringAsync("api/FileUploads/bytImage?imageName=1");
            var result = JsonConvert.DeserializeObject<byte[]>(json);

            var ad = new List<AdsM>() { new AdsM() { Image = result } };
            return await Task.FromResult(ad);
        }
        public Task<bool> AddItemAsync(AdsM item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<bool> UpdateItemAsync(AdsM item)
        {
            throw new NotImplementedException();
        }
    }
}
