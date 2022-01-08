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
        HttpClient client;

        public BookRestService()
        {
            client = new HttpClient();
        }

        public string RefreshDataAsync()
        {
            Uri uri = new Uri(string.Format(Constants.GoogleBooksUrl, string.Empty));
            HttpResponseMessage response = client.GetAsync(uri).Result;
            string content = "";
            if (response.IsSuccessStatusCode)
            {
                content = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(content);
            }
            return content;
        }
    }
}
