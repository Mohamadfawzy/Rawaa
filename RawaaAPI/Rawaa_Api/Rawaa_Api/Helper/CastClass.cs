using Newtonsoft.Json;

namespace Rawaa_Api.Helper
{
    public static class CastClass
    {
        public static To? Deserialize<From,To>(From from,To to)
        {
            return JsonConvert.DeserializeObject<To>(JsonConvert.SerializeObject(from));
        }
    }
}
