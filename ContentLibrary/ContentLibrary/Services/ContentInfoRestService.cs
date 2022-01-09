namespace ContentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Text;

    internal class ContentInfoRestService : IRestService
    {
        readonly HttpClient client;
        public ContentInfoRestService()
        {
            client = new HttpClient();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaType">movie, podcast, music, musicVideo, audiobook, shortFilm, tvShow, software, ebook, all. Default: all</param>
        /// <param name="searchTerm">The unescaped term to be search</param>
        /// <returns></returns>
        public string GetDataAsync(string mediaType, string searchTerm)
        {
            Uri uri = new Uri(Constants.AppleiTunesUri + Uri.EscapeUriString(searchTerm) + $"&media={mediaType}");
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
