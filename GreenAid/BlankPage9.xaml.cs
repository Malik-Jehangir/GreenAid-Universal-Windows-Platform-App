using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class BlankPage9 : Page
    {
        public BlankPage9()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage6));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmailMessage emailmessge = new EmailMessage();
            emailmessge.To.Add(new EmailRecipient("mmmcgloughlin@ucdavis.edu"));
            emailmessge.Subject = "GreenAid-help mail";
            //attachments here
            await EmailManager.ShowComposeNewEmailAsync(emailmessge);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
           
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EmailMessage emailmessge = new EmailMessage();
            emailmessge.To.Add(new EmailRecipient("lemauxpg@nature.berkeley.edu"));
            emailmessge.Subject = "GreenAid-help mail";
            //attachments here
            await EmailManager.ShowComposeNewEmailAsync(emailmessge);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox 
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EmailMessage emailmessge = new EmailMessage();
            emailmessge.To.Add(new EmailRecipient("Trewavas@ed.ac.uk"));
            emailmessge.Subject = "GreenAid-help mail";
            //attachments here
            await EmailManager.ShowComposeNewEmailAsync(emailmessge);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox 
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            EmailMessage emailmessge = new EmailMessage();
            emailmessge.To.Add(new EmailRecipient("ingo@potrykus.net"));
            emailmessge.Subject = "GreenAid-help mail";
            //attachments here
            await EmailManager.ShowComposeNewEmailAsync(emailmessge);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox 
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            EmailMessage emailmessge = new EmailMessage();
            emailmessge.To.Add(new EmailRecipient("wparrott@uga.cc.uga.edu"));
            emailmessge.Subject = "GreenAid-help mail";
            //attachments here
            await EmailManager.ShowComposeNewEmailAsync(emailmessge);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox 
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            EmailMessage emailmessge = new EmailMessage();
            emailmessge.To.Add(new EmailRecipient("fwambugu@ahbfi.or.ke"));
            emailmessge.Subject = "GreenAid-help mail";
            //attachments here
            await EmailManager.ShowComposeNewEmailAsync(emailmessge);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox 
        }
    }
}
