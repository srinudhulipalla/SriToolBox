using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using Microsoft.Phone.Tasks;

namespace SriToolBox
{
    public partial class MainPage : PhoneApplicationPage
    {        
        PanoramaItemModel PanoramaItem = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Device_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PanoramaItem = ((sender as ListBox).SelectedValue as PanoramaItemModel);
            if (PanoramaItem == null) return;

            switch (PanoramaItem.ID)
            {
                case 1:
                    this.NavigationService.Navigate(new Uri("/Device/SystemInfo.xaml", UriKind.Relative));                    
                    break;
                case 2:
                    MessageBox.Show("call history");
                    break;
                case 3:
                    MessageBox.Show("message history");
                    break;
                case 4:
                    this.NavigationService.Navigate(new Uri("/Device/Multimedia.xaml", UriKind.Relative));  
                    break;
                case 5:
                    MessageBox.Show("data usage");
                    break;
                case 6:
                    MessageBox.Show("apps");
                    break;
            }
        }

        private void Tools_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PanoramaItem = ((sender as ListBox).SelectedValue as PanoramaItemModel);
            if (PanoramaItem == null) return;

            switch (PanoramaItem.ID)
            {
                case 1:
                    MessageBox.Show("flashlight");
                    break;
            }
        }

        private void Finance_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {       
            PanoramaItem = ((sender as ListBox).SelectedValue as PanoramaItemModel);
            if (PanoramaItem == null) return;

            switch (PanoramaItem.ID)
            {
                case 1: 
                    this.NavigationService.Navigate(new Uri("/Finance/CalcInterest.xaml", UriKind.Relative));
                    break;
                case 2:
                    this.NavigationService.Navigate(new Uri("/Finance/CalcTax.xaml", UriKind.Relative));
                    break;
            }     
        }

        private void Others_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {     
            PanoramaItem = ((sender as ListBox).SelectedValue as PanoramaItemModel);
            if (PanoramaItem == null) return;

            switch (PanoramaItem.ID)
            {
                case 1:
                    this.NavigationService.Navigate(new Uri("/Device/About.xaml", UriKind.Relative));
                    break;
                case 2: 
                    EmailComposeTask emailTask = new EmailComposeTask();
                    emailTask.To = AppSettings.ToEmail;
                    emailTask.Subject = string.Format("{0} v{1} - Feedback", AppSettings.AppName, Common.GetAppVersion());
                    emailTask.Show();
                    break;
            }            
        }
        
    }
}