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
    public sealed partial class BlankPage5 : Page
    {
        public BlankPage5()
        {
            this.InitializeComponent();
        }
        String E;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            E = e.Parameter.ToString();
        }
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(NameUser.Text=="" || AddressUser.Text=="" || ContactUser.Text=="")
            {
             MessageDialog msgbox = new MessageDialog("Please complete all the feilds ....");
             await msgbox.ShowAsync();
            }
            else
            {
                
                Frame.Navigate(typeof(BlankPage4), NameUser.Text + "\n\nDelivery Destination: " + AddressUser.Text + "\n\nContact: " + ContactUser.Text + "\n\nTotal Amount: " + E);
            }
           
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

     
    }
}
