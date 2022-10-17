using System.Text.Json;

namespace Rawaa_Api.Helper
{
    public static class CastClass
    {
        public static To? Deserialize<From,To>(From from,To to)
        {
            return JsonSerializer.Deserialize<To>(JsonSerializer.Serialize(from));
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
