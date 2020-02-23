using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Email;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GreenAid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        String E;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            E = e.Parameter.ToString();

            Geolocator geo = new Geolocator();
            String fsClient = "54Y0QWTJHE51STOYJZLW5VKF5SNVZWGG1CSVJLVCAXTKL0LU";
            String fssecret = "0ZAYE2TMNCNIMIEYCFUQXPWR15NS0FO0T2LCABU4XK0RBYB5";
            Geoposition pos = await geo.GetGeopositionAsync(); // get the raw geoposition data
            double lat = pos.Coordinate.Point.Position.Latitude; // current latitude
            double longt = pos.Coordinate.Point.Position.Longitude;
            try
            {
                string datatopost = "?client_id=" + fsClient + "&client_secret=" + fssecret + "&v=20130815&ll=" + lat + "," + longt + "&query=flower";
                Uri address = new Uri("https://api.foursquare.com/v2/venues/search" + datatopost);
                // Create the web request
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                // Set type to POST
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                // Create the data we want to send
                request.Accept = "application/json";
                // Get response
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //To obtain response body
                        using (Stream streamResponse = response.GetResponseStream())
                        {
                            using (StreamReader streamRead = new StreamReader(streamResponse, Encoding.UTF8))
                            {
                                RootObject nearbyVenues = JsonConvert.DeserializeObject<RootObject>(streamRead.ReadToEnd().ToString());
                                nearbyPlaces.ItemsSource = nearbyVenues.response.venues;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("Some error occured", "Outrika");
                await dialog.ShowAsync();
            }
        }

        private async void OnPostItemClick(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox = new MessageDialog("Please click the attachment option to attach your order and then click send.");
            EmailMessage emailmessge = new EmailMessage();
            emailmessge.To.Add(new EmailRecipient("jehangir.mj@gmail.com"));
            emailmessge.Subject = "GreenAid-Order mail";
            emailmessge.Body = "This is a test email\n" + E + "\n";
            //attachments here
            await EmailManager.ShowComposeNewEmailAsync(emailmessge);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
            await msgbox.ShowAsync();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
    public class Meta
    {
        public int code { get; set; }
    }

    public class Contact
    {
        public string phone { get; set; }
        public string formattedPhone { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string facebookName { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public int distance { get; set; }
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public List<string> formattedAddress { get; set; }
        public string address { get; set; }
        public string crossStreet { get; set; }
        public string postalCode { get; set; }
    }

    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        public Icon icon { get; set; }
        public bool primary { get; set; }
    }

    public class Stats
    {
        public int checkinsCount { get; set; }
        public int usersCount { get; set; }
        public int tipCount { get; set; }
    }

    public class Specials
    {
        public int count { get; set; }
        public List<object> items { get; set; }
    }

    public class HereNow
    {
        public int count { get; set; }
        public string summary { get; set; }
        public List<object> groups { get; set; }
    }

    public class Menu
    {
        public string type { get; set; }
        public string label { get; set; }
        public string anchor { get; set; }
        public string url { get; set; }
        public string mobileUrl { get; set; }
        public string externalUrl { get; set; }
    }

    public class Provider
    {
        public string name { get; set; }
    }

    public class Delivery
    {
        public string url { get; set; }
        public Provider provider { get; set; }
    }

    public class Reservations
    {
        public string url { get; set; }
    }

    public class VenuePage
    {
        public string id { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Contact contact { get; set; }
        public Location location { get; set; }
        public List<Category> categories { get; set; }
        public bool verified { get; set; }
        public Stats stats { get; set; }
        public Specials specials { get; set; }
        public HereNow hereNow { get; set; }
        public string referralId { get; set; }
        public bool? hasMenu { get; set; }
        public Menu menu { get; set; }
        public string url { get; set; }
        public Delivery delivery { get; set; }
        public Reservations reservations { get; set; }
        public VenuePage venuePage { get; set; }
    }

    public class Response
    {
        public List<Venue> venues { get; set; }
    }

    public class RootObject
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
    }
}
