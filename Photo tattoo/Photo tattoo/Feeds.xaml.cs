using System;
using System.Linq;
using System.Net;
using Microsoft.Phone.Controls;
using Photo_tattoo.Classes;
using System.Xml.Linq;
using Windows.Networking.Connectivity;
using System.Windows;



namespace Photo_tattoo
{
    public partial class Feeds : PhoneApplicationPage
    {
        private bool Is_Connected {get; set;}
        public Feeds()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            bool conected = UpdateNetworkInformation();
            if (conected == false)
            {
                MessageBox.Show("Internet is not accessible!", "Heads up", MessageBoxButton.OK);
                NavigationService.GoBack();
            }
            else
            {
                WebClient rssClient = new WebClient();
                rssClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(rssClient_DownloadStringCompleted);
                rssClient.Encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                rssClient.DownloadStringAsync(new Uri(@"http://www.tatmash.com/media_feeds/all.xml"));
            }
        }
        void rssClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var rssData = from rss in XElement.Parse(e.Result).Descendants("item")
                          select new FeedsClass
                          {
                              title = rss.Element("title").Value,
                              image = (rss.Element("enclosure") == null) ? "" : (string)rss.Element("enclosure").Attribute("url").Value
                          };
            ListFeed.ItemsSource = rssData;
            progress.Visibility = System.Windows.Visibility.Collapsed;
        }

        public bool UpdateNetworkInformation()
        {
            // Get current Internet Connection Profile.
            ConnectionProfile internetConnectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();

            //air plan mode is on...
            if (internetConnectionProfile == null)
            {
                Is_Connected = false;
                return Is_Connected;
            }
            //if true, internet is accessible.
            else
                return true;
        }
    }
}