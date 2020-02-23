using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Media.MediaProperties;
using Windows.Devices.Enumeration;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Graphics.Imaging;
using Windows.Graphics.Display;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using System.Reflection;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GreenAid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage3 : Page
    {
        
        private NavigationHelper navigationHelper;  

        private FileSavePickerContinuationEventArgs _filePickerEventArgs = null;

        public FileSavePickerContinuationEventArgs FilePickerEvent
        {
            get { return _filePickerEventArgs; }
            set
            {
                _filePickerEventArgs = value;
                ContinueFileSavePicker(_filePickerEventArgs);
            }
        }
        public BlankPage3()
        {
            this.InitializeComponent();
       

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

        }

       
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        private async void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }

        #region NavigationHelper registration


         MediaCapture captureManager = new MediaCapture();
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            Capture.Visibility = Visibility.Visible;

            var cameraID = await GetCameraID(Windows.Devices.Enumeration.Panel.Back);


            await captureManager.InitializeAsync(new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Video,
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview,
                AudioDeviceId = string.Empty,
                VideoDeviceId = cameraID.Id
            });

            var maxResolution = captureManager.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo).Aggregate((i1, i2) => (i1 is VideoEncodingProperties ? (i1 as VideoEncodingProperties).Width : 0) > (i2 is VideoEncodingProperties ? (i2 as VideoEncodingProperties).Width : 0) ? i1 : i2);
            await captureManager.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, maxResolution);
            // rotate to see preview vertically
          captureManager.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
            capturePreview.Source = captureManager;
            await captureManager.StartPreviewAsync();

           
        }
        #endregion

        private static async Task<DeviceInformation> GetCameraID(Windows.Devices.Enumeration.Panel desiredCamera)
        {
            // get available devices for capturing pictures
            DeviceInformation deviceID = (await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture))
                .FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredCamera);

            if (deviceID != null) return deviceID;
            else throw new Exception(string.Format("Camera of type {0} doesn't exist.", desiredCamera));
        }
        
       
        

        async private void TakePhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            Price.Visibility = Visibility.Visible;
            Rect.Visibility = Visibility.Visible;
            b1.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Visible;
            Capture.Visibility = Visibility.Collapsed;
            

           
            // create a file
            StorageFile photoFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("myFirstPhoto.jpg", CreationCollisionOption.ReplaceExisting);
           
            // take a photo with choosen Encoding
            await captureManager.CapturePhotoToStorageFileAsync(ImageEncodingProperties.CreateJpeg(), photoFile);
            

            // show a photo on screen
            ImageBrush brush= new ImageBrush();
            
            BitmapImage bitmapToShow = new BitmapImage(new Uri(photoFile.Path));
            //imagePreivew.Source = bitmapToShow;  // show image on screen inside Image control defined in XAML
            brush.ImageSource = bitmapToShow;
            //MainGrid.Background = brush;
            
            Mainstack.Background = brush;
            capturePreview.Visibility = Visibility.Collapsed;
            captureManager.Dispose();
          
        }
        public void Dispose()
        {
            if (captureManager !=  null)
            {
                captureManager.Dispose();
                captureManager = null;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

            products.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Visible;
            
            Pricetext.Visibility = Visibility.Collapsed;
           
           
            Price.Visibility = Visibility.Collapsed;
        }
        //CUSTOMIZE COLOR
        private void PA1_click(object sender, RoutedEventArgs e)
        {
            SUB1.Visibility = Visibility.Visible;

        }
        private void PB1_click(object sender, RoutedEventArgs e)
        {
            SUB2.Visibility = Visibility.Visible;
        }

        private void PC1_click(object sender, RoutedEventArgs e)
        {
            SUB3.Visibility = Visibility.Visible;
        }
        private void PD1_click(object sender, RoutedEventArgs e)
        {
            SUB4.Visibility = Visibility.Visible;
        }
        private void PE1_click(object sender, RoutedEventArgs e)
        {
            SUB5.Visibility = Visibility.Visible;
        }
        private void PF1_click(object sender, RoutedEventArgs e)
        {
            SUB6.Visibility = Visibility.Visible;
        }
        private void PG1_click(object sender, RoutedEventArgs e)
        {
            SUB7.Visibility = Visibility.Visible;
        }
        private void PH1_click(object sender, RoutedEventArgs e)
        {
            SUB8.Visibility = Visibility.Visible;
        }
        private void PI1_click(object sender, RoutedEventArgs e)
        {
            SUB9.Visibility = Visibility.Visible;
        }
        private void PA2_click(object sender, RoutedEventArgs e)
        {
            SUB10.Visibility = Visibility.Visible;
        }
        private void PB2_click(object sender, RoutedEventArgs e)
        {
            SUB11.Visibility = Visibility.Visible;
        }
        private void PC2_click(object sender, RoutedEventArgs e)
        {
            SUB12.Visibility = Visibility.Visible;
        }
        private void PD2_click(object sender, RoutedEventArgs e)
        {
            SUB13.Visibility = Visibility.Visible;
        }
        private void PE2_click(object sender, RoutedEventArgs e)
        {
            SUB14.Visibility = Visibility.Visible;
        }
        private void PF2_click(object sender, RoutedEventArgs e)
        {
            SUB15.Visibility = Visibility.Visible;
        }
        private void PG2_click(object sender, RoutedEventArgs e)
        {
            SUB16.Visibility = Visibility.Visible;
        }
        private void PH2_click(object sender, RoutedEventArgs e)
        {
            SUB17.Visibility = Visibility.Visible;
        }
        private void PI2_click(object sender, RoutedEventArgs e)
        {
            SUB18.Visibility = Visibility.Visible;
        }
        private void PA3_click(object sender, RoutedEventArgs e)
        {
            SUB19.Visibility = Visibility.Visible;
        }
        private void PB3_click(object sender, RoutedEventArgs e)
        {
            SUB20.Visibility = Visibility.Visible;
        }
        private void PC3_click(object sender, RoutedEventArgs e)
        {
            SUB21.Visibility = Visibility.Visible;
        }
        private void PD3_click(object sender, RoutedEventArgs e)
        {
            SUB22.Visibility = Visibility.Visible;
        }
        private void PE3_click(object sender, RoutedEventArgs e)
        {
            SUB23.Visibility = Visibility.Visible;
        }
        private void PF3_click(object sender, RoutedEventArgs e)
        {
            SUB24.Visibility = Visibility.Visible;
        }
        private void PG3_click(object sender, RoutedEventArgs e)
        {
            SUB25.Visibility = Visibility.Visible;
        }
        private void PH3_click(object sender, RoutedEventArgs e)
        {
            SUB26.Visibility = Visibility.Visible;
        }
        private void PI3_click(object sender, RoutedEventArgs e)
        {
            SUB27.Visibility = Visibility.Visible;
        }

        //PLANT TAPPING EVENT THAT WILL MAKE THINGS VISIBLE ON SCREEN//////////////////////////////////////////////////////////////////////
        /////////P1
        double x = 0.00;
        int g1 = 0;
        private void P11_click(object sender, RoutedEventArgs e)
        {
            g1 = g1 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            SUB1.Visibility = Visibility.Collapsed;

          
            Price.Visibility = Visibility.Visible;
               x = x + 3.00;
               P11.Visibility = Visibility.Visible; 
               Price.Text = "$ " + x;
               
        } //P12,P23,P24 TO BE WRITTEN
        
        /////////P2
        int g2 = 0;
        private void P21_click(object sender, RoutedEventArgs e)
        {
            g2 = g2 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB2.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
          

                x = x + 3.00;
                P21.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;
        }//P22,23,24 TO BE WRITTEN
        //P3
        int g3 = 0;
        private void P31_click(object sender, RoutedEventArgs e)
        {
            g3 = g3 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
                x = x + 3.00;
                P31.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;

        }
        //p4
        int g4 = 0;
        private void P41_click(object sender, RoutedEventArgs e)
        {
            g4 = g4 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            SUB4.Visibility = Visibility.Collapsed;

            
           
            Price.Visibility = Visibility.Visible;
          
                x = x + 3.00;
                P41.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;

          
        }
        //P5
        int g5 = 0;
        private void P51_click(object sender, RoutedEventArgs e)
        {
            g5 = g5 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            SUB5.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                P51.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;


        }
        //P6
        int g6 = 0;
        private  void P61_click(object sender, RoutedEventArgs e)
        {
            g6 = g6 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB6.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
                P61.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;


        }
        //P7
        int g7 = 0;
        private void P71_click(object sender, RoutedEventArgs e)
        {
            g7 = g7 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB7.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
                P71.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;

           

        }
        //P8
        int g8 = 0;
        private  void P81_click(object sender, RoutedEventArgs e)
        {
            g8 = g8 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB8.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
                P81.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;
            
        }
        //P9
        int g9 = 0;
        private  void P91_click(object sender, RoutedEventArgs e)
        {
            g9 = g9 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB9.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;

            x = x + 3.00;
                P91.Visibility = Visibility.Visible;
                Price.Text = "$ " + x;

        }
        int g10 = 0;
        private void P101_click(object sender, RoutedEventArgs e)
        {
            g10 = g10 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB10.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
           
           
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P101.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g11 = 0;
        private void P111_click(object sender, RoutedEventArgs e)
        {
            g11 = g11 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB11.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;


            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P111.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g12 = 0;
        private void P121_click(object sender, RoutedEventArgs e)
        {
            g12 = g12 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB12.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;

            
           
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P121.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g13 = 0;
        private void P131_click(object sender, RoutedEventArgs e)
        {
            g13 = g13 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB13.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;


            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P131.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g14 = 0;
        private void P141_click(object sender, RoutedEventArgs e)
        {
            g14 = g8 + 1;
            products3.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            SUB14.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P141.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g15 = 0;
        private void P151_click(object sender, RoutedEventArgs e)
        {
            g15 = g15 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB15.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P151.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g16 = 0;
        private void P12_click(object sender, RoutedEventArgs e)
        {
            g16 = g16 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB1.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P12.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g17 = 0;
        private void P13_click(object sender, RoutedEventArgs e)
        {
            g17 = g17 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB1.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P13.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g18 = 0;
        private void P14_click(object sender, RoutedEventArgs e)
        {
            g18 = g18 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB1.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P14.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g19 = 0;
        private void P22_click(object sender, RoutedEventArgs e)
        {
            g19 = g19 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB2.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P22.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g20 = 0;
        private void P23_click(object sender, RoutedEventArgs e)
        {
            g20 = g20 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB2.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P23.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g21 = 0;
        private void P24_click(object sender, RoutedEventArgs e)
        {
            g21 = g21 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB2.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;

            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P24.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g22 = 0;
        private void P32_click(object sender, RoutedEventArgs e)
        {
            g22 = g22 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB3.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P32.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g23 = 0;
        private void P33_click(object sender, RoutedEventArgs e)
        {
            g23 = g23 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB3.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P33.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g24 = 0;
        private void P34_click(object sender, RoutedEventArgs e)
        {
            g24 = g24 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB3.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P34.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g25 = 0;
        private void P42_click(object sender, RoutedEventArgs e)
        {
            g25 = g25 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB4.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P42.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g26 = 0;
        private void P43_click(object sender, RoutedEventArgs e)
        {
            g26 = g26 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB4.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
           productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P43.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g27 = 0;
        private void P44_click(object sender, RoutedEventArgs e)
        {
            g27 = g27 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB4.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P44.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g28 = 0;
        private void P52_click(object sender, RoutedEventArgs e)
        {
            g28 = g28 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB5.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P52.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g29 = 0;
        private void P53_click(object sender, RoutedEventArgs e)
        {
           g29 = g29 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB5.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P53.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g30 = 0;
        private void P54_click(object sender, RoutedEventArgs e)
        {
            g30 = g30 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB5.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P54.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g31 = 0;
        private void P62_click(object sender, RoutedEventArgs e)
        {
            g31 = g31 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB6.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P62.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g32 = 0;
        private void P63_click(object sender, RoutedEventArgs e)
        {
            g32 = g32 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB6.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P63.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g33 = 0;
        private void P64_click(object sender, RoutedEventArgs e)
        {
            g33 = g33 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB6.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P64.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g34 = 0;
        private void P72_click(object sender, RoutedEventArgs e)
        {
            g34 = g34 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB7.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P72.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g35 = 0;
        private void P73_click(object sender, RoutedEventArgs e)
        {
            g35 = g35 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB7.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P73.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g36 = 0;
        private void P74_click(object sender, RoutedEventArgs e)
        {
            g36 = g36 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB7.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P74.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g37 = 0;
        private void P82_click(object sender, RoutedEventArgs e)
        {
            g37 = g37 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB8.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P82.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g38 = 0;
        private void P83_click(object sender, RoutedEventArgs e)
        {
            g38 = g38 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB8.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P83.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g39 = 0;
        private void P84_click(object sender, RoutedEventArgs e)
        {
            g39 = g39 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB8.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P84.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g40 = 0;
        private void P92_click(object sender, RoutedEventArgs e)
        {
            g40 = g40 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB9.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P92.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g41 = 0;
        private void P93_click(object sender, RoutedEventArgs e)
        {
            g41 = g41 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB9.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P93.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g42 = 0;
        private void P94_click(object sender, RoutedEventArgs e)
        {

            g42 = g42 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB9.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P94.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g43 = 0;
        private void P102_click(object sender, RoutedEventArgs e)
        {
            g43 = g43 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB10.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P102.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g44 = 0;
        private void P103_click(object sender, RoutedEventArgs e)
        {
            g44 = g44 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB10.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P103.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g45 = 0;
        private void P104_click(object sender, RoutedEventArgs e)
        {
            g45 = g45 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB10.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P104.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g46 = 0;
        private void P112_click(object sender, RoutedEventArgs e)
        {
            g46 = g46 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB11.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P112.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g47 = 0;
        private void P113_click(object sender, RoutedEventArgs e)
        {
            g47 = g47 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB11.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P113.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g48 = 0;
        private void P114_click(object sender, RoutedEventArgs e)
        {
            g48 = g48 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB11.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P114.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g49 = 0;
        private void P122_click(object sender, RoutedEventArgs e)
        {
            g49 = g49 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB12.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P122.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g50 = 0;
        private void P123_click(object sender, RoutedEventArgs e)
        {
            g50 = g50 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB12.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P123.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g51 = 0;
        private void P124_click(object sender, RoutedEventArgs e)
        {
            g51 = g51 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB12.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P124.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g52 = 0;
        private void P132_click(object sender, RoutedEventArgs e)
        {
            g52 = g52 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB13.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P132.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g53 = 0;
        private void P133_click(object sender, RoutedEventArgs e)
        {
            g53 = g53 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB13.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P133.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g54 = 0;
        private void P134_click(object sender, RoutedEventArgs e)
        {
            g54 = g54 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB13.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P134.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g55 = 0;
        private void P142_click(object sender, RoutedEventArgs e)
        {
            g55 = g55 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB14.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P142.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g56 = 0;
        private void P143_click(object sender, RoutedEventArgs e)
        {
            g56 = g56 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB14.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P143.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g57 = 0;
        private void P144_click(object sender, RoutedEventArgs e)
        {
            g57 = g57 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB14.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P144.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g58 = 0;
        private void P152_click(object sender, RoutedEventArgs e)
        {
            g58 = g58 + 1;
            products3.Visibility = Visibility.Collapsed;
            productMain.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB15.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;

            x = x + 3.00;
            P152.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g59 = 0;
        private void P153_click(object sender, RoutedEventArgs e)
        {
            g59 = g59 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB15.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P153.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g60 = 0;
        private void P154_click(object sender, RoutedEventArgs e)
        {
            g60 = g60 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB15.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P154.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g61 = 0;
        private void P161_click(object sender, RoutedEventArgs e)
        {
            g61 = g61 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB16.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P161.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g62 = 0;
        private void P162_click(object sender, RoutedEventArgs e)
        {
            g62 = g62 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB16.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P162.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g63 = 0;
        private void P163_click(object sender, RoutedEventArgs e)
        {
            g63 = g63 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB16.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P163.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g64 = 0;
        private void P164_click(object sender, RoutedEventArgs e)
        {
            g64 = g64 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB16.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P164.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g65 = 0;
        private void P171_click(object sender, RoutedEventArgs e)
        {
            g65 = g65 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB17.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P171.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g66 = 0;
        private void P172_click(object sender, RoutedEventArgs e)
        {
            g66 = g66 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB17.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P172.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g67 = 0;
        private void P173_click(object sender, RoutedEventArgs e)
        {
            g67 = g67 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB17.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P173.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g68 = 0;
        private void P174_click(object sender, RoutedEventArgs e)
        {
            g68 = g68 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB17.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P174.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g69 = 0;
        private void P181_click(object sender, RoutedEventArgs e)
        {
            g69 = g69 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB18.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P181.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g70 = 0;
        private void P182_click(object sender, RoutedEventArgs e)
        {
            g70 = g70 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB18.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P182.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g71 = 0;
        private void P183_click(object sender, RoutedEventArgs e)
        {
            g71 = g71 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB18.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P183.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g72 = 0;
        private void P184_click(object sender, RoutedEventArgs e)
        {
            g72 = g72 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB18.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            P184.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        ///////////////////////////////////////fLOWERS////////////////////////////////////////////////
        int g73 = 0;
        private void F11_click(object sender, RoutedEventArgs e)
        {
            g73 = g73 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB19.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F11.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g74 = 0;
        private void F12_click(object sender, RoutedEventArgs e)
        {
            g74 = g74 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB19.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F12.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g75 = 0;
        private void F13_click(object sender, RoutedEventArgs e)
        {
            g75 = g75 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB19.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F13.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g76 = 0;
        private void F14_click(object sender, RoutedEventArgs e)
        {
            g76 = g76 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB19.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F14.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g77 = 0;
        private void F21_click(object sender, RoutedEventArgs e)
        {
            g77 = g77 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB20.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F21.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g78 = 0;
        private void F22_click(object sender, RoutedEventArgs e)
        {
            g78 = g78 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB20.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F22.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g79 = 0;
        private void F23_click(object sender, RoutedEventArgs e)
        {
            g79 = g79 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB20.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F23.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g107 = 0;
        private void F24_click(object sender, RoutedEventArgs e)
        {
            g107 = g107 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB20.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F24.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g106 = 0;
        private void F31_click(object sender, RoutedEventArgs e)
        {
            g106 = g106 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB21.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F31.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g80 = 0;
        private void F32_click(object sender, RoutedEventArgs e)
        {
            g80 = g80 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB21.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F32.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g81 = 0;
        private void F33_click(object sender, RoutedEventArgs e)
        {
            g81 = g81 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB21.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F33.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g82 = 0;
        private void F34_click(object sender, RoutedEventArgs e)
        {
            g82 = g82 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB21.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F34.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g83 = 0;
        private void F41_click(object sender, RoutedEventArgs e)
        {
            g83 = g83 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB22.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F41.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g108 = 0;
        private void F42_click(object sender, RoutedEventArgs e)
        {
            g108 = g108 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB22.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F42.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g84 = 0;
        private void F43_click(object sender, RoutedEventArgs e)
        {
            g84 = g84 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB22.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F43.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g85 = 0;
        private void F44_click(object sender, RoutedEventArgs e)
        {
            g85 = g85 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB22.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F44.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g86 = 0;
        private void F51_click(object sender, RoutedEventArgs e)
        {
            g86 = g86 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB23.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F51.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g87 = 0;
        private void F52_click(object sender, RoutedEventArgs e)
        {
            g87 = g87 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB23.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F52.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g88 = 0;
        private void F53_click(object sender, RoutedEventArgs e)
        {
            g88 = g88 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB23.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F53.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g89 = 0;
        private void F54_click(object sender, RoutedEventArgs e)
        {
            g89 = g89 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB23.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F54.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g90 = 0;
        private void F61_click(object sender, RoutedEventArgs e)
        {
            g90 = g90 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB24.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F61.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g91 = 0;
        private void F62_click(object sender, RoutedEventArgs e)
        {
            g91 = g91 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB24.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F62.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g92 = 0;
        private void F63_click(object sender, RoutedEventArgs e)
        {
            g92 = g92 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB24.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F63.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g93 = 0;
        private void F64_click(object sender, RoutedEventArgs e)
        {
            g93 = g93 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB24.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F64.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g94 = 0;
        private void F71_click(object sender, RoutedEventArgs e)
        {
            g94 = g94 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB25.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F71.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g95 = 0;
        private void F72_click(object sender, RoutedEventArgs e)
        {
            g95 = g95 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB25.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F72.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g96 = 0;
        private void F73_click(object sender, RoutedEventArgs e)
        {
            g96 = g96 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB25.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F73.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g97 = 0;
        private void F74_click(object sender, RoutedEventArgs e)
        {
            g97 = g97 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB25.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F74.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g98 = 0;
        private void F81_click(object sender, RoutedEventArgs e)
        {
            g98 = g98 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB26.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F81.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g99 = 0;
        private void F82_click(object sender, RoutedEventArgs e)
        {
            g99 = g99 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB26.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F82.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g100 = 0;
        private void F83_click(object sender, RoutedEventArgs e)
        {
            g100 = g100 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB26.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F83.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g101 = 0;
        private void F84_click(object sender, RoutedEventArgs e)
        {
            g101 = g101 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB26.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F84.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g102 = 0;
        private void F91_click(object sender, RoutedEventArgs e)
        {
            g102 = g102 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB27.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F91.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g103 = 0;
        private void F92_click(object sender, RoutedEventArgs e)
        {
            g103 = g103 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB27.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F92.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g104 = 0;
        private void F93_click(object sender, RoutedEventArgs e)
        {
            g104 = g104 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB27.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F93.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        int g105 = 0;
        private void F94_click(object sender, RoutedEventArgs e)
        {
            g105 = g105 + 1;
            products3.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            SUB27.Visibility = Visibility.Collapsed;
            Pricetext.Visibility = Visibility.Visible;
            productMain.Visibility = Visibility.Collapsed;
            Price.Visibility = Visibility.Visible;
            products4.Visibility = Visibility.Collapsed;
            x = x + 3.00;
            F94.Visibility = Visibility.Visible;
            Price.Text = "$ " + x;

        }
        ///////////////////////ACCESSORIES///////////////////////////////////
        int g110 = 0;
        private async void PA4_click(object sender, RoutedEventArgs e)
        {
            g110 = g110 + 1;
            if(g110==1) {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a11.Visibility = Visibility.Visible;
                
            }
            else if (g110 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a12.Visibility = Visibility.Visible;
                
            }
            else if (g110 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a13.Visibility = Visibility.Visible;
               
            }
            else if (g110 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a14.Visibility = Visibility.Visible;
                
            }
            else if (g110 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }

        int g111 = 0;
        private async void PB4_click(object sender, RoutedEventArgs e)
        {
            g111 = g111 + 1;
            if (g111 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a21.Visibility = Visibility.Visible;

            }
            else if (g111 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a22.Visibility = Visibility.Visible;

            }
            else if (g111 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a23.Visibility = Visibility.Visible;

            }
            else if (g111 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a24.Visibility = Visibility.Visible;

            }
            else if (g111 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }
        int g112 = 0;
        private async void PC4_click(object sender, RoutedEventArgs e)
        {
            g112 = g112 + 1;
            if (g112 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 13.00;
                a31.Visibility = Visibility.Visible;

            }
            else if (g112 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 13.00;
                a32.Visibility = Visibility.Visible;

            }
            else if (g112 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 13.00;
                a33.Visibility = Visibility.Visible;

            }
            else if (g112 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 13.00;
                a34.Visibility = Visibility.Visible;

            }
            else if (g112 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }
        int g113 = 0;
        private async void PD4_click(object sender, RoutedEventArgs e)
        {
            g113 = g113 + 1;
            if (g113 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 150.00;
                a41.Visibility = Visibility.Visible;

            }
            else if (g113 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 150.00;
                a42.Visibility = Visibility.Visible;

            }
            else if (g113 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 150.00;
                a43.Visibility = Visibility.Visible;

            }
            else if (g113 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 150.00;
                a44.Visibility = Visibility.Visible;

            }
            else if (g113 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }
        int g114 = 0;
        private async void PE4_click(object sender, RoutedEventArgs e)
        {
            g114 = g114 + 1;
            if (g114 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a51.Visibility = Visibility.Visible;

            }
            else if (g114 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a52.Visibility = Visibility.Visible;

            }
            else if (g114 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a53.Visibility = Visibility.Visible;

            }
            else if (g114 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a54.Visibility = Visibility.Visible;

            }
            else if (g114 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }
        int g115 = 0;
        private async void PF4_click(object sender, RoutedEventArgs e)
        {
            g115 = g115 + 1;
            if (g115 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a61.Visibility = Visibility.Visible;

            }
            else if (g115 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a62.Visibility = Visibility.Visible;

            }
            else if (g115 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a63.Visibility = Visibility.Visible;

            }
            else if (g115 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 20.00;
                a64.Visibility = Visibility.Visible;

            }
            else if (g115 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }
        int g116 = 0;
        private async void PG4_click(object sender, RoutedEventArgs e)
        {
            g116 = g116 + 1;
            if (g116 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 3.00;
                a71.Visibility = Visibility.Visible;

            }
            else if (g116 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 10.00;
                a72.Visibility = Visibility.Visible;

            }
            else if (g116 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 10.00;
                a73.Visibility = Visibility.Visible;

            }
            else if (g116 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 10.00;
                a74.Visibility = Visibility.Visible;

            }
            else if (g116 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }

        int g117 = 0;
        private async void PH4_click(object sender, RoutedEventArgs e)
        {
            g117 = g117 + 1;
            if (g117 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a81.Visibility = Visibility.Visible;

            }
            else if (g117 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a82.Visibility = Visibility.Visible;

            }
            else if (g117 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a83.Visibility = Visibility.Visible;

            }
            else if (g117 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a84.Visibility = Visibility.Visible;

            }
            else if (g117 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }

        int g118 = 0;
        private async void PI4_click(object sender, RoutedEventArgs e)
        {
            g118 = g118 + 1;
            if (g118 == 1)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a91.Visibility = Visibility.Visible;

            }
            else if (g118 == 2)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a92.Visibility = Visibility.Visible;

            }
            else if (g118 == 3)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a93.Visibility = Visibility.Visible;

            }
            else if (g118 == 4)
            {
                products3.Visibility = Visibility.Collapsed;
                products.Visibility = Visibility.Collapsed;
                products2.Visibility = Visibility.Collapsed;
                products4.Visibility = Visibility.Collapsed;
                Pricetext.Visibility = Visibility.Visible;
                productMain.Visibility = Visibility.Collapsed;
                Price.Visibility = Visibility.Visible;

                x = x + 40.00;
                a94.Visibility = Visibility.Visible;

            }
            else if (g118 >= 5)
            {
                MessageDialog msgbox = new MessageDialog("Cannot add more, please make a different selection, thankyou.");
                await msgbox.ShowAsync();
            }
            Price.Text = "$ " + x;
        }
        ///////////////////////////////////////Double tapped//////////////////////////////////////////
        private void P11_Dclick(object sender, RoutedEventArgs e)
        {
            P11.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
       private void P12_Dclick(object sender, RoutedEventArgs e)
        {
            P12.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P13_Dclick(object sender, RoutedEventArgs e)
        {
            P13.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P14_Dclick(object sender, RoutedEventArgs e)
        {
            P14.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P21_Dclick(object sender, RoutedEventArgs e)
        {
            P21.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P22_Dclick(object sender, RoutedEventArgs e)
        {
            P22.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P23_Dclick(object sender, RoutedEventArgs e)
        {
            P23.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P24_Dclick(object sender, RoutedEventArgs e)
        {
            P24.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P31_Dclick(object sender, RoutedEventArgs e)
        {
            P31.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P32_Dclick(object sender, RoutedEventArgs e)
        {
            P32.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P33_Dclick(object sender, RoutedEventArgs e)
        {
            P33.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P34_Dclick(object sender, RoutedEventArgs e)
        {
            P34.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P51_Dclick(object sender, RoutedEventArgs e)
        {
            P51.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
       private void P52_Dclick(object sender, RoutedEventArgs e)
        {
            P52.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P53_Dclick(object sender, RoutedEventArgs e)
        {
            P53.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P54_Dclick(object sender, RoutedEventArgs e)
        {
            P54.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P61_Dclick(object sender, RoutedEventArgs e)
        {
            P61.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P62_Dclick(object sender, RoutedEventArgs e)
        {
            P62.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P63_Dclick(object sender, RoutedEventArgs e)
        {
            P63.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P64_Dclick(object sender, RoutedEventArgs e)
        {
            P64.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P71_Dclick(object sender, RoutedEventArgs e)
        {
            P71.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P72_Dclick(object sender, RoutedEventArgs e)
        {
            P72.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P73_Dclick(object sender, RoutedEventArgs e)
        {
            P73.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P74_Dclick(object sender, RoutedEventArgs e)
        {
            P74.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P81_Dclick(object sender, RoutedEventArgs e)
        {
            P81.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P82_Dclick(object sender, RoutedEventArgs e)
        {
            P82.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P83_Dclick(object sender, RoutedEventArgs e)
        {
            P83.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P84_Dclick(object sender, RoutedEventArgs e)
        {
            P84.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P91_Dclick(object sender, RoutedEventArgs e)
        {
            P91.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P92_Dclick(object sender, RoutedEventArgs e)
        {
            P92.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P93_Dclick(object sender, RoutedEventArgs e)
        {
            P93.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P94_Dclick(object sender, RoutedEventArgs e)
        {
            P94.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P41_Dclick(object sender, RoutedEventArgs e)
        {
            P41.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P42_Dclick(object sender, RoutedEventArgs e)
         {
             P42.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P43_Dclick(object sender, RoutedEventArgs e)
         {
             P43.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P44_Dclick(object sender, RoutedEventArgs e)
         {
             P44.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
        private void P101_Dclick(object sender, RoutedEventArgs e)
        {
            P101.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P102_Dclick(object sender, RoutedEventArgs e)
         {
             P102.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P103_Dclick(object sender, RoutedEventArgs e)
         {
             P103.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P104_Dclick(object sender, RoutedEventArgs e)
         {
             P104.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
        private void P111_Dclick(object sender, RoutedEventArgs e)
        {
            P111.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P112_Dclick(object sender, RoutedEventArgs e)
         {
             P112.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P113_Dclick(object sender, RoutedEventArgs e)
         {
             P113.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P114_Dclick(object sender, RoutedEventArgs e)
         {
             P114.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
        private void P121_Dclick(object sender, RoutedEventArgs e)
        {
            P121.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P122_Dclick(object sender, RoutedEventArgs e)
         {
             P122.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P123_Dclick(object sender, RoutedEventArgs e)
         {
             P123.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P124_Dclick(object sender, RoutedEventArgs e)
         {
             P124.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
        private void P131_Dclick(object sender, RoutedEventArgs e)
        {
            P131.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P132_Dclick(object sender, RoutedEventArgs e)
         {
             P132.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P133_Dclick(object sender, RoutedEventArgs e)
         {
             P133.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P134_Dclick(object sender, RoutedEventArgs e)
         {
             P134.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
        private void P141_Dclick(object sender, RoutedEventArgs e)
        {
            P141.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P142_Dclick(object sender, RoutedEventArgs e)
         {
             P142.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P143_Dclick(object sender, RoutedEventArgs e)
         {
             P143.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P144_Dclick(object sender, RoutedEventArgs e)
         {
             P144.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
        private void P151_Dclick(object sender, RoutedEventArgs e)
        {
            P151.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P152_Dclick(object sender, RoutedEventArgs e)
         {
             P152.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P153_Dclick(object sender, RoutedEventArgs e)
         {
             P153.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
         private void P154_Dclick(object sender, RoutedEventArgs e)
         {
             P154.Visibility = Visibility.Collapsed;
             x = x - 3.00;
             Price.Text = "$ " + x;
         }
        private void P161_Dclick(object sender, RoutedEventArgs e)
        {
            P161.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P162_Dclick(object sender, RoutedEventArgs e)
        {
            P162.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P163_Dclick(object sender, RoutedEventArgs e)
        {
            P163.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P164_Dclick(object sender, RoutedEventArgs e)
        {
            P164.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P171_Dclick(object sender, RoutedEventArgs e)
        {
            P171.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P172_Dclick(object sender, RoutedEventArgs e)
        {
            P172.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P173_Dclick(object sender, RoutedEventArgs e)
        {
            P173.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P174_Dclick(object sender, RoutedEventArgs e)
        {
            P174.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P181_Dclick(object sender, RoutedEventArgs e)
        {
            P181.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P182_Dclick(object sender, RoutedEventArgs e)
        {
            P182.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P183_Dclick(object sender, RoutedEventArgs e)
        {
            P183.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void P184_Dclick(object sender, RoutedEventArgs e)
        {
            P184.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }

        ///////////////////////DELETION FOR FLOWERS///////////////////////////////////
        private void F11_Dclick(object sender, RoutedEventArgs e)
        {
            F11.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F12_Dclick(object sender, RoutedEventArgs e)
        {
            F12.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F13_Dclick(object sender, RoutedEventArgs e)
        {
            F13.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F14_Dclick(object sender, RoutedEventArgs e)
        {
            F14.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F21_Dclick(object sender, RoutedEventArgs e)
        {
            F21.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F22_Dclick(object sender, RoutedEventArgs e)
        {
            F22.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F23_Dclick(object sender, RoutedEventArgs e)
        {
            F23.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F24_Dclick(object sender, RoutedEventArgs e)
        {
            F24.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F31_Dclick(object sender, RoutedEventArgs e)
        {
            F31.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F32_Dclick(object sender, RoutedEventArgs e)
        {
            F32.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F33_Dclick(object sender, RoutedEventArgs e)
        {
            F33.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F34_Dclick(object sender, RoutedEventArgs e)
        {
            F34.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F41_Dclick(object sender, RoutedEventArgs e)
        {
            F41.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F42_Dclick(object sender, RoutedEventArgs e)
        {
            F42.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F43_Dclick(object sender, RoutedEventArgs e)
        {
            F43.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F44_Dclick(object sender, RoutedEventArgs e)
        {
            F44.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F51_Dclick(object sender, RoutedEventArgs e)
        {
            F51.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F52_Dclick(object sender, RoutedEventArgs e)
        {
            F52.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F53_Dclick(object sender, RoutedEventArgs e)
        {
            F53.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F54_Dclick(object sender, RoutedEventArgs e)
        {
            F54.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F61_Dclick(object sender, RoutedEventArgs e)
        {
            F61.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F62_Dclick(object sender, RoutedEventArgs e)
        {
            F62.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F63_Dclick(object sender, RoutedEventArgs e)
        {
            F63.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F64_Dclick(object sender, RoutedEventArgs e)
        {
            F64.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F71_Dclick(object sender, RoutedEventArgs e)
        {
            F71.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F72_Dclick(object sender, RoutedEventArgs e)
        {
            F72.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F73_Dclick(object sender, RoutedEventArgs e)
        {
            F73.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F74_Dclick(object sender, RoutedEventArgs e)
        {
            F74.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
      
        private void F81_Dclick(object sender, RoutedEventArgs e)
        {
            F81.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F82_Dclick(object sender, RoutedEventArgs e)
        {
            F82.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F83_Dclick(object sender, RoutedEventArgs e)
        {
            F83.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F84_Dclick(object sender, RoutedEventArgs e)
        {
            F84.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F91_Dclick(object sender, RoutedEventArgs e)
        {
            F91.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F92_Dclick(object sender, RoutedEventArgs e)
        {
            F92.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F93_Dclick(object sender, RoutedEventArgs e)
        {
            F93.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void F94_Dclick(object sender, RoutedEventArgs e)
        {
            F94.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        //////////////////////////////////////accessories removal//////////////////////////////////////////
        private void a11_Dclick(object sender, RoutedEventArgs e)
        {
            a11.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a12_Dclick(object sender, RoutedEventArgs e)
        {
            a12.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a13_Dclick(object sender, RoutedEventArgs e)
        {
            a13.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a14_Dclick(object sender, RoutedEventArgs e)
        {
            a14.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a21_Dclick(object sender, RoutedEventArgs e)
        {
            a21.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a22_Dclick(object sender, RoutedEventArgs e)
        {
            a22.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a23_Dclick(object sender, RoutedEventArgs e)
        {
            a23.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a24_Dclick(object sender, RoutedEventArgs e)
        {
            a24.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a31_Dclick(object sender, RoutedEventArgs e)
        {
            a31.Visibility = Visibility.Collapsed;
            x = x - 13.00;
            Price.Text = "$ " + x;
        }
        private void a32_Dclick(object sender, RoutedEventArgs e)
        {
            a32.Visibility = Visibility.Collapsed;
            x = x - 13.00;
            Price.Text = "$ " + x;
        }
        private void a33_Dclick(object sender, RoutedEventArgs e)
        {
            a33.Visibility = Visibility.Collapsed;
            x = x - 13.00;
            Price.Text = "$ " + x;
        }
        private void a34_Dclick(object sender, RoutedEventArgs e)
        {
            a34.Visibility = Visibility.Collapsed;
            x = x - 13.00;
            Price.Text = "$ " + x;
        }
        private void a41_Dclick(object sender, RoutedEventArgs e)
        {
            a41.Visibility = Visibility.Collapsed;
            x = x - 150.00;
            Price.Text = "$ " + x;
        }
        private void a42_Dclick(object sender, RoutedEventArgs e)
        {
            a42.Visibility = Visibility.Collapsed;
            x = x - 150.00;
            Price.Text = "$ " + x;
        }
        private void a43_Dclick(object sender, RoutedEventArgs e)
        {
            a43.Visibility = Visibility.Collapsed;
            x = x - 150.00;
            Price.Text = "$ " + x;
        }
        private void a44_Dclick(object sender, RoutedEventArgs e)
        {
            a44.Visibility = Visibility.Collapsed;
            x = x - 150.00;
            Price.Text = "$ " + x;
        }
        private void a51_Dclick(object sender, RoutedEventArgs e)
        {
            a51.Visibility = Visibility.Collapsed;
            x = x - 3.00;
            Price.Text = "$ " + x;
        }
        private void a52_Dclick(object sender, RoutedEventArgs e)
        {
            a52.Visibility = Visibility.Collapsed;
            x = x - 20.00;
            Price.Text = "$ " + x;
        }
        private void a53_Dclick(object sender, RoutedEventArgs e)
        {
            a53.Visibility = Visibility.Collapsed;
            x = x - 20.00;
            Price.Text = "$ " + x;
        }
        private void a54_Dclick(object sender, RoutedEventArgs e)
        {
            a54.Visibility = Visibility.Collapsed;
            x = x - 20.00;
            Price.Text = "$ " + x;
        }
        private void a61_Dclick(object sender, RoutedEventArgs e)
        {
            a61.Visibility = Visibility.Collapsed;
            x = x - 20.00;
            Price.Text = "$ " + x;
        }
        private void a62_Dclick(object sender, RoutedEventArgs e)
        {
            a62.Visibility = Visibility.Collapsed;
            x = x - 20.00;
            Price.Text = "$ " + x;
        }
        private void a63_Dclick(object sender, RoutedEventArgs e)
        {
            a63.Visibility = Visibility.Collapsed;
            x = x - 20.00;
            Price.Text = "$ " + x;
        }
        private void a64_Dclick(object sender, RoutedEventArgs e)
        {
            a64.Visibility = Visibility.Collapsed;
            x = x - 20.00;
            Price.Text = "$ " + x;
        }
        private void a71_Dclick(object sender, RoutedEventArgs e)
        {
            a71.Visibility = Visibility.Collapsed;
            x = x - 10.00;
            Price.Text = "$ " + x;
        }
        private void a72_Dclick(object sender, RoutedEventArgs e)
        {
            a72.Visibility = Visibility.Collapsed;
            x = x - 10.00;
            Price.Text = "$ " + x;
        }
        private void a73_Dclick(object sender, RoutedEventArgs e)
        {
           a73.Visibility = Visibility.Collapsed;
            x = x - 10.00;
            Price.Text = "$ " + x;
        }
        private void a74_Dclick(object sender, RoutedEventArgs e)
        {
            a74.Visibility = Visibility.Collapsed;
            x = x - 10.00;
            Price.Text = "$ " + x;
        }
        private void a81_Dclick(object sender, RoutedEventArgs e)
        {
            a81.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }
        private void a82_Dclick(object sender, RoutedEventArgs e)
        {
            a82.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }
        private void a83_Dclick(object sender, RoutedEventArgs e)
        {
            a83.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }
        private void a84_Dclick(object sender, RoutedEventArgs e)
        {
            a84.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }
        private void a91_Dclick(object sender, RoutedEventArgs e)
        {
            a91.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }
        private void a92_Dclick(object sender, RoutedEventArgs e)
        {
            a92.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }
        private void a93_Dclick(object sender, RoutedEventArgs e)
        {
            a93.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }
        private void a94_Dclick(object sender, RoutedEventArgs e)
        {
            a94.Visibility = Visibility.Collapsed;
            x = x - 40.00;
            Price.Text = "$ " + x;
        }

        //SCREENSHOT 
        //Creates RenderTargetBitmap from UI Element 

        private async Task<RenderTargetBitmap> CreateBitmapFromElement(FrameworkElement uielement)
        {
            try
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(uielement);
                return renderTargetBitmap;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }
        //Creates RenderTargetBitmap from UI Element 
        async Task<RenderTargetBitmap> CaptureToStreamAsync(FrameworkElement uielement, IRandomAccessStream stream, Guid encoderId)
        {
            try
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(uielement);
                var pixels = await renderTargetBitmap.GetPixelsAsync();
                var logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
                var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    logicalDpi,
                    logicalDpi,
                    pixels.ToArray());

                await encoder.FlushAsync();
                return renderTargetBitmap;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }

            return null;
        }

    
      
      
        private async void Button_Click_In(object sender, RoutedEventArgs e)
        {

            MessageDialog msgbox = new MessageDialog("Some quotes to gear things up: \n\n\n“The clearest way into the Universe is through a forest wilderness.”― John Muir\n\n\n“I want my words to illuminate like the sun, as I give my daily lecture on photosynthesis to my houseplants.” ― Jarod Kintz\n\n\n I Want\n“We are made for loving. If we don’t love, we will be like plants without water.” ― Desmond Tutu\n\n\nTell me of what plant-birthday a man takes notice, and I shall tell you a good deal about his vocation, his hobbies, his hay fever, and the general level of his ecological education.” ― Aldo Leopold");
            await msgbox.ShowAsync();

        }
      
        private async void done_Click(object sender, RoutedEventArgs e)
        {
            products.Visibility = Visibility.Collapsed;
            products2.Visibility = Visibility.Collapsed;
            if(Price.Text == "$ 0")
            {
                MessageDialog msgbox = new MessageDialog("You haven't selected any items to order !! Please select some items to continue...");
                await msgbox.ShowAsync();

            }
            else
            {
            CreateFileSavePicker();
            }
        }
        void CreateFileSavePicker()
        {
            //Create the picker object
            FileSavePicker savePicker = new FileSavePicker();

            // Dropdown of file types the user can save the file as   
            savePicker.FileTypeChoices.Add
                (
                "Image", new List<string>() { ".png" });
            // Default file name if the user does not type one in or select // a file to replace
            savePicker.SuggestedFileName = "ORDER";
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            //// Open the picker for the user to pick a file
            savePicker.ContinuationData["Operation"] = "SomeDataOrOther";
            savePicker.PickSaveFileAndContinue();

        }
        public async void ContinueFileSavePicker(Windows.ApplicationModel.Activation.FileSavePickerContinuationEventArgs args)
        {

            StorageFile file = args.File;
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);

                Guid encoderId = BitmapEncoder.JpegEncoderId;

                try
                {
                    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await CaptureToStreamAsync(MainGrid, stream, encoderId);
                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message);
                }
                // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);

                if (status == FileUpdateStatus.Complete)
                {
                    MessageDialog msgbox = new MessageDialog("File " + file.Name + " was saved");
                    //Calling the Show method of MessageDialog class  
                    //which will show the MessageBox  
                    await msgbox.ShowAsync();
                    Frame.Navigate(typeof(BlankPage5),Price.Text);
                   
                }
                else
                {
                    MessageDialog msgbox = new MessageDialog("File " + file.Name + " was not saved.");
                    await msgbox.ShowAsync();
                    Frame.Navigate(typeof(MainPage));
                }
            }
            else
            {
                MessageDialog msgbox = new MessageDialog("Operation cancelled.");
                await msgbox.ShowAsync();
            }
        }
        //ZOOM AND DRAGING 
        void control_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Image img = sender as Image;
            CompositeTransform ct = img.RenderTransform as CompositeTransform;
            ct.ScaleX *= e.Delta.Scale;
            ct.ScaleY *= e.Delta.Scale;
            ct.TranslateX += e.Delta.Translation.X;
            ct.TranslateY += e.Delta.Translation.Y;
        }
        async void DisplayMessage(string error)
        {
            var dialog = new MessageDialog(error);

            await dialog.ShowAsync();
        }
      
        private async void  Instruct_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msgbox = new MessageDialog("To design your place: \nClick the 'Add plant' option. \nTo remove an item, just double click. \nThen save the photo to your gallery.\nFollow the next instructions.  ");
            await msgbox.ShowAsync();
           
        }
        private  void Indoor_Click(object sender, RoutedEventArgs e)
        {
            products2.Visibility = Visibility.Visible;
            products.Visibility = Visibility.Collapsed;
            products3.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;
        }
        private  void Outdoor_Click(object sender, RoutedEventArgs e)
        {
            products.Visibility = Visibility.Visible;
            products2.Visibility = Visibility.Collapsed;
            products3.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;

        }
        private void Flower_click(object sender, RoutedEventArgs e)
        {

            products3.Visibility = Visibility.Visible;
            products2.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products4.Visibility = Visibility.Collapsed;

        }
        private void asset_click(object sender, RoutedEventArgs e)
        {

            products4.Visibility = Visibility.Visible;
            products2.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Collapsed;
            products3.Visibility = Visibility.Collapsed;

        }

    }

}
