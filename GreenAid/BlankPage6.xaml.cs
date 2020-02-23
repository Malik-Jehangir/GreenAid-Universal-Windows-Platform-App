using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GreenAid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage6 : Page
    {
        public BlankPage6()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage7));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox = new MessageDialog("Coming soon ");
            await msgbox.ShowAsync();
        }
        private void EXIT(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Button_Click_Ins(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage2));
        }

        private void GreenBlog(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage8));
        }

        private void Experts(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage9));
        }

        private void Workshops(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage10));
        }
        private void Button_Click_Plant(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage11));
        }
    }
}
