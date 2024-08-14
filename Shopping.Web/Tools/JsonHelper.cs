using System.Text.Json;

namespace Shopping.Web.Tools
{
    public class JsonHelper
    {
        private static readonly JsonSerializerOptions DefaultOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static T? Deserialize<T> (string? json, JsonSerializerOptions? options = null)
        {
            if (string.IsNullOrWhiteSpace (json))
            {
                return default;
            }

            if (options == null)
            {
                options = DefaultOptions;
            }

            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static string Serialize<T> (T? obj, JsonSerializerOptions? options = null)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (options == null)
            {
                options = DefaultOptions;
            }

            return JsonSerializer.Serialize(obj, options);
        }
    }
}
