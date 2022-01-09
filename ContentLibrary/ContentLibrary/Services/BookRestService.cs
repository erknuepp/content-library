namespace ContentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    internal class BookRestService : IRestService
    {
        readonly HttpClient client;

        public BookRestService()
        {
            client = new HttpClient();
        }

        public string GetDataAsync(string mediaType, string searchTerm)
        {

            Uri uri = new Uri(Constants.GoogleBooksUri + Uri.EscapeUriString(searchTerm) + Constants.MaxResults + Constants.ApiKey);
            HttpResponseMessage response = client.GetAsync(uri).Result;
            string content = "";
            if (response.IsSuccessStatusCode)
            {
                content = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(content, mediaType);
            }
            else
            {
                return response.StatusCode.ToString();
            }
            return content;
        }
    }
}
