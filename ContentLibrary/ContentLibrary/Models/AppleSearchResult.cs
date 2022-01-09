namespace ContentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class AppleSearchResult
    {

        public int resultCount { get; set; }
        public Result[] results { get; set; }

    }

    internal class Result
    {
        public string artworkUrl60 { get; set; }
        public string artworkUrl100 { get; set; }
        public string artistViewUrl { get; set; }
        public string trackCensoredName { get; set; }
        public int fileSizeBytes { get; set; }
        public string formattedPrice { get; set; }
        public string trackViewUrl { get; set; }
        public DateTime releaseDate { get; set; }
        public string[] genreIds { get; set; }
        public int trackId { get; set; }
        public string trackName { get; set; }
        public int[] artistIds { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public int artistId { get; set; }
        public string artistName { get; set; }
        public string[] genres { get; set; }
        public float price { get; set; }
        public string kind { get; set; }
        public float averageUserRating { get; set; }
        public int userRatingCount { get; set; }
    }
}
