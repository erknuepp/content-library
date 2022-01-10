﻿namespace ContentLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        ContentLibrary contentLibrary;

        public MainPage()
        {
            InitializeComponent();

            contentLibrary = new ContentLibrary();

            contentTypePicker.ItemsSource = new List<string>
            {
                "Book",
                "AudioBook",
                "Movie",
                "Album",
                "Song"
            };            
        }

        public void AddContentButtonClicked(object sender, EventArgs e)
        {
            var contentType = contentTypePicker.SelectedItem as string;
            var title = contentTitle.Text;

            switch (contentType)
            {
                case "Book":
                    contentLibrary.Contents.Add(new Book(title));
                    break;
                case "AudioBook":
                    contentLibrary.Contents.Add(new AudioBook(title));
                    break;
                case "Movie":
                    contentLibrary.Contents.Add(new Movie(title));
                    contentLibrary.Contents.Add(new Movie(title + " w/ Subtitles", new Subtitle(new Dictionary<string, string> { { "SceneOne", "In a world" } })));
                    break;
                case "Album":
                    contentLibrary.Contents.Add(new Album(title, new List<Song> { new Song("Song One"), new Song("Song Two") }));
                    break;
                case "Song":
                    contentLibrary.Contents.Add(new Song(title));
                    break;
                default:
                    break;
            }
            contentTypePicker.SelectedIndex = -1;
            contentTitle.Text = "";
            contentLibraryListView.ItemsSource = contentLibrary.Contents;
        }

        public void PlayButtonClicked(object sender, EventArgs e)
        {
            var selectedContentItem = contentLibraryListView.SelectedItem;
            if (selectedContentItem != null)
            {
                if (selectedContentItem is Movie movie)
                {
                    stateMessage.Text = movie.Play();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("movie", movie.GetTitle());
                    DisplayContentInfo(content);
                }
                else if (selectedContentItem is Song song)
                {
                    stateMessage.Text = song.Play();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("music", song.GetTitle());
                    DisplayContentInfo(content);
                }
                else if (selectedContentItem is AudioBook audioBook)
                {
                    stateMessage.Text = audioBook.Play();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("audiobook", audioBook.GetTitle());
                    DisplayContentInfo(content);
                }
                else if (selectedContentItem is Album album)
                {
                    stateMessage.Text = album.Play();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("music", album.GetTitle());
                    DisplayContentInfo(content);
                }
                else
                {
                    stateMessage.Text = $"The Play action is not supported for {selectedContentItem.GetType().Name} Content Type.";
                }
            }
        }

        public void ReadButtonClicked(object sender, EventArgs e)
        {
            var selectedContentItem = contentLibraryListView.SelectedItem;
            if (selectedContentItem != null)
            {
                if (selectedContentItem is Book book)
                {
                    stateMessage.Text = book.Read();
                    //IRestService restService = new BookRestService();
                    //var content = restService.GetDataAsync("book", book.GetTitle());
                    //Volume volume = JsonSerializer.Deserialize<Volume>(content);
                    //api.Source = volume.items.First().accessInfo.webReaderLink;
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("ebook", book.GetTitle());
                    DisplayContentInfo(content);


                }
                else if (selectedContentItem is Movie movie)
                {
                    stateMessage.Text = movie.Read();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("movie", movie.GetTitle());
                    DisplayContentInfo(content);
                }
                else
                {
                    stateMessage.Text = $"The Read action is not supported for {selectedContentItem.GetType().Name} Content Type.";
                }
            }
        }

        public void ViewButtonClicked(object sender, EventArgs e)
        {
            var selectedContentItem = contentLibraryListView.SelectedItem;
            if (selectedContentItem != null)
            {
                if (selectedContentItem is Movie movie)
                {
                    stateMessage.Text = movie.View();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("movie", movie.GetTitle());
                    DisplayContentInfo(content);
                }
                else
                {
                    stateMessage.Text = $"The View action is not supported for {selectedContentItem.GetType().Name} Content Type.";
                }
            }
        }

        public void ListenButtonClicked(object sender, EventArgs e)
        {
            var selectedContentItem = contentLibraryListView.SelectedItem;
            if (selectedContentItem != null)
            {
                if (selectedContentItem is AudioBook audioBook)
                {
                    stateMessage.Text = audioBook.Listen();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("audiobook", audioBook.GetTitle());
                    DisplayContentInfo(content);
                }
                else if(selectedContentItem is Song song)
                {
                    stateMessage.Text = song.Listen();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("music", song.GetTitle());
                    DisplayContentInfo(content);
                }
                else if(selectedContentItem is Album album)
                {
                    stateMessage.Text = album.Listen();
                    IRestService contentInfoService = new ContentInfoRestService();
                    var content = contentInfoService.GetDataAsync("music", album.GetTitle());
                    DisplayContentInfo(content);
                }
                else
                {
                    stateMessage.Text = $"The Listen action is not supported for {selectedContentItem.GetType().Name} Content Type.";
                }
            }
        }

        public void DisposeButtonClicked(object sender, EventArgs e)
        {
            contentLibrary.Dispose();
            contentLibraryListView.ItemsSource = null;
            contentLibraryListView.ItemsSource = contentLibrary.Contents;
        }

        public void SearchContentButtonClicked(object sender, EventArgs e)
        {
            var title = contentTitle.Text;
            if (contentLibrary.Contents.Select(x => x.GetTitle()).Contains(title))
            {
                stateMessage.Text = $"{title} was found";
            }
            else
            {
                stateMessage.Text = $"{title} was not found, you can select a content type and it.";
            }
        }

        private void DisplayContentInfo(string content)
        {
            var result = (JsonSerializer.Deserialize<AppleSearchResult>(content)).results.FirstOrDefault();
            contentImage.Source = result.artworkUrl100;
            contentName.Text = result.artistName;
            contentDescription.Text = result.trackName;
        }
    }
}
