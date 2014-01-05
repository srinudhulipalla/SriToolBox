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

            switch (PanoramaItem.ID)
            {
                case 1:
                    this.NavigationService.Navigate(new Uri("/Device/SystemInfo.xaml", UriKind.Relative));                    
                    break;
                case 4:
                    this.NavigationService.Navigate(new Uri("/Device/Multimedia.xaml", UriKind.Relative));  
                    break;
            }
        }

        private void Finance_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {       
            PanoramaItem = ((sender as ListBox).SelectedValue as PanoramaItemModel);

            switch (PanoramaItem.ID)
            {
                case 1: this.NavigationService.Navigate(new Uri("/Finance/CalcInterest.xaml", UriKind.Relative));
                    break;
                case 2: MessageBox.Show("Calc Tax");
                    break;
            }     
        }

        private void Others_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {     
            PanoramaItem = ((sender as ListBox).SelectedValue as PanoramaItemModel);

            switch (PanoramaItem.ID)
            {
                case 1: MessageBox.Show("About");
                    break;
                case 2: MessageBox.Show("Feedback");
                    break;
            }            
        }

        void DeviceStatus_PowerSourceChanged(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(() => {
                if (DeviceStatus.PowerSource.ToString() != "External")
                {
                    MessageBox.Show("Device is disconnected from an external power source.");
                }
            });
        }

        void DeviceDisconnectedFromPower()
        {
            if (DeviceStatus.PowerSource.ToString() != "External")
            {
                MessageBox.Show("Device is disconnected from an external power source.");
            }
        }

        
    }
}