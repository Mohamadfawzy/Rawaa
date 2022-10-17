using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Rawaa.Services;
using Xamarin.Forms;
using System.IO;
using Rawaa.Models;


namespace Rawaa.Services
{
    public class RequestProvider<T>
    {
       
        private const string BaseUrl = AppSettings.ApiUrl;
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        HttpClient client;


        public RequestProvider()
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            //client = new HttpClient(httpClientHandler)

            client = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = new TimeSpan(0, 0, 20)
            };
        }

        // post single 
        public async Task<T> PostOneAsync<Take>(Take item, string uri, string token = "")
        {
            var valueReturned = default(T);// as ResponseResult<TResult>;

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                //valueReturned.Status = true;
                var resJson = response.Content.ReadAsStringAsync().Result;
                valueReturned = await Task.Run(() => JsonConvert.DeserializeObject<T>(resJson));

                return valueReturned; // default(TResult);
            }
            else
            {
                Console.WriteLine(response);
                await AppSettings.Alert(response.RequestMessage.ToString());
            }
            return default(T);
        }


        // get list
        public async Task<List<T>> GetListAsync(string uri = "", string token = "null")
        {
            try
            {
                var responseMessage = await client.GetAsync(uri);
                if (responseMessage == null)
                    return null;

                var content = await responseMessage.Content.ReadAsStringAsync();
                var listFromT = JsonConvert.DeserializeObject<List<T>>(content);
                return await Task.FromResult(listFromT);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        // Get single  by ID
        public async Task<T> GetById(string uri, string id, string token = "")
        {
            var valueReturned = default(T);
            try
            {
                var response = await client.GetAsync(uri + id);

                if (!response.IsSuccessStatusCode)
                {
                    return valueReturned;
                }

                var json = await response.Content.ReadAsStringAsync();
                var listFromT = JsonConvert.DeserializeObject<T>(json);
                return await Task.FromResult(listFromT);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return valueReturned;
        }

        // remove 
        public async Task<bool> DeleteOneAsync(string uri, string id="", string token = "")
        {
            try
            {
                var response = await client.DeleteAsync(uri + id);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var Json = await response.Content.ReadAsStringAsync();
                //T model = JsonConvert.DeserializeObject<T>(Json);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return false;
        }

        // update
        // post single 
        public async Task<T> PutOneAsync<Take>(Take item, string uri, string token = "")
        {
            var valueReturned = default(T);// as ResponseResult<TResult>;

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                //valueReturned.Status = true;
                var resJson = response.Content.ReadAsStringAsync().Result;
                valueReturned = await Task.Run(() => JsonConvert.DeserializeObject<T>(resJson));

                return valueReturned; // default(TResult);
            }
            else
            {
                Console.WriteLine(response);
            }
            return default(T);
        }



        #region
        // get range
        public async Task<TResult> GetRangeAsync<TResult>(string uri, int skipe, int take, string token = "")
        {
            TResult result;
            try
            {
                var json = await client.GetAsync(uri);
                var content = await json.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TResult>(content);
                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return default(TResult);// await Task.FromResult(default(TResult));
        }

        public async Task<TResult> GetOneAsync<TResult>(string uri, Guid id, string token = "")
        {
            try
            {
                client.DefaultRequestHeaders.Add("id", id.ToString());
                var json = await client.GetStringAsync("Students");
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(json));
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TResult> PostOneAsync<TResult, Take>(Take item, string uri, string token = "")
        {
            //TResult result;
            var valueReturned = default(TResult);// as ResponseResult<TResult>;

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                //valueReturned.Status = true;
                var resJson = response.Content.ReadAsStringAsync().Result;
                valueReturned = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(resJson));

                return valueReturned; // default(TResult);
            }
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return default(TResult);
        }

        // remove 
        public async Task<TResult> DeleteOneAsync<TResult>(Guid id, string uri, string token = "")
        {
            //var json = JsonConvert.SerializeObject(item);
            // HttpResponseMessage response = await client.DeleteAsync(uri);
            //var content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
            try
            {
                //Uri uri1 = new Uri(string.Format(uri, id));

                var fullUri = Path.Combine(uri, id.ToString());

                var response = await client.DeleteAsync(fullUri);

                if (!response.IsSuccessStatusCode)
                {
                    return default(TResult);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var resJson = response.Content.ReadAsStringAsync().Result;
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(resJson));
                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return default(TResult);
        }
        #endregion

    } // end class
}




/* Deleted
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
[assembly: Dependency(typeof(RequestProvider))]
: IRequestProvider<TResult>
*/