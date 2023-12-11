namespace PDFMonkeyOnDotnet
{
    using Newtonsoft.Json.Linq;

    using System.Text;

    public class Caller<T> where T : class
    {
        private readonly HttpClient client = new();

        public Caller(string apiSecretKey)
        {
            if (client.DefaultRequestHeaders.Any(h => h.Key == "Authorization"))
                return;

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiSecretKey}");
        }

        public async Task<T> Get(string path, string identifier)
        {
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode == false)
                throw new Exception($"Error occurred when retrieving data from PDFMonkey server with error code: {response.StatusCode} and {response.ReasonPhrase}");

            var json = await response.Content.ReadAsStringAsync();

            if (json == null)
                throw new Exception($"Error occurred when reading data from PDFMonkey server!");

            var result = JObject.Parse(json)[identifier]?.ToObject<T>();

            if (result == null)
                throw new Exception($"Error occurred when casting data from PDFMonkey server to Object type: {identifier}!");

            return result;
        }

        public async Task<T> PostAsync(string path, string content, string identifier)
        {

            var jsonContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(path, jsonContent);

            if (response.IsSuccessStatusCode == false)
                throw new Exception($"Error occurred when sending data to PDFMonkey server with error code: {response.StatusCode} and {response.ReasonPhrase}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(jsonResult)[identifier]?.ToObject<T>();

            if (result == null)
                throw new Exception($"Error occurred when casting data from PDFMonkey server to Object type: {identifier}!");

            return result;
        }
    }
}