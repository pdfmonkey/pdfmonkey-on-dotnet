namespace Helpers
{
    public static class DownloadFileHelper
    {
        public static async Task DownloadFileAsync(string fileUrl, string destinationPath)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
            using var httpStream = await response.Content.ReadAsStreamAsync();
            await httpStream.CopyToAsync(fileStream);
        }
    }
}