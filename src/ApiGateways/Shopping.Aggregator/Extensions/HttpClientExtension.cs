using System.Text.Json;

namespace Shopping.Aggregator.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Something went wrong calling the API: {response.ReasonPhrase}");

            var datastring = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(datastring, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
