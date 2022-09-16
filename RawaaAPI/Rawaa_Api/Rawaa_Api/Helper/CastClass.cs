using Newtonsoft.Json;

namespace Rawaa_Api.Helper
{
    public static class CastClass
    {
        public static To? Deserialize<From,To>(From from,To to)
        {
            return JsonConvert.DeserializeObject<To>(JsonConvert.SerializeObject(from));
        }
        public static bool IsNullOrEmpty(string[] fields)
        {
            foreach(var field in fields)
            {
                if(string.IsNullOrEmpty(field))
                    return true;
            }
            return false;
        }
    }
}
