using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Specs.Library.Drivers.Api
{
    public static class JsonExtensions
    {
        public static string AsJson(this object entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        public static StringContent AsJsonContent(this string entity)
        {
            return new StringContent(entity, Encoding.UTF8, "application/json");
        }

        public static StringContent AsJsonContent(this object entity)
        {
            var payload = entity.AsJson();
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }
}