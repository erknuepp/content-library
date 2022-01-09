namespace ContentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Constants
    {
        //Google Books API
        public const string GoogleBooksUri = "https://www.googleapis.com/books/v1/volumes?q=";
        public const string MaxResults = "&maxResults=1";
        public const string ApiKey = "&key=AIzaSyCEMDg8gRalxPF4d_FNAwWN5BWz-tMsgcQ";

        //Apple iTunes API
        public const string AppleiTunesUri = "https://itunes.apple.com/search?limit=1&term=";
    }
}
