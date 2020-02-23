using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace GreenAid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage: Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.Loaded += PageLoaded;

        }
        void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (Images != null)
            {
                playlistTimer.Start();
                count++;
                if (count < Images.Count)
                {
                    ImageRotation();
                }
                else
                {
                    count = 0;
                    ImageRotation();
                }
            }

        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        DispatcherTimer playlistTimer = null;
        List<string> Images = new List<string>();
        int count = 0;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Images.Add("a.jpg");
            Images.Add("b.jpg");
            Images.Add("c.jpg");
            Images.Add("d.jpg");
            Images.Add("e.jpg");
            Images.Add("a.jpg");
            playlistTimer = new DispatcherTimer();
            playlistTimer.Interval = new TimeSpan(0, 0, 3);
            playlistTimer.Tick += playlistTimer_Tick;
            image.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + Images[count]));
        }

        void playlistTimer_Tick(object sender, object e)
        {
            if (Images != null)
            {
                if (count == Images.Count - 1)
                    count = 0;
                if (count < Images.Count)
                {
                    count++;
                    ImageRotation();
                }
            }
        }
        private void ImageRotation()
        {
           image.Source = new BitmapImage(new Uri("ms-appx:///Assets/" + Images[count].ToString()));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            Frame.Navigate(typeof(BlankPage3));

        }

        private void More_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage6));
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
        

        private async void Feed_Click(object sender, RoutedEventArgs e)
        {


            await Launcher.LaunchUriAsync(new Uri("http://kwiksurveys.com/s/jJfzU06a"));


        }

    }
}
