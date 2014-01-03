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
        FrameworkElement UIElement = null;
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
            UIElement = e.OriginalSource as FrameworkElement;
            PanoramaItem = UIElement.DataContext as PanoramaItemModel;

            switch (PanoramaItem.ID)
            {
                case 1:
                    this.NavigationService.Navigate(new Uri("/Device/SystemInfo.xaml", UriKind.Relative));
                    //string name = "ApplicationCurrentMemoryUsage: " + DeviceStatus.ApplicationCurrentMemoryUsage + Environment.NewLine;
                    //name += "ApplicationMemoryUsageLimit: " + DeviceStatus.ApplicationMemoryUsageLimit + Environment.NewLine;
                    //name += "ApplicationPeakMemoryUsage: " + DeviceStatus.ApplicationPeakMemoryUsage + Environment.NewLine;
                    //name += "DeviceFirmwareVersion: " + DeviceStatus.DeviceFirmwareVersion + Environment.NewLine;
                    //name += "DeviceHardwareVersion: " + DeviceStatus.DeviceHardwareVersion + Environment.NewLine;
                    //name += "DeviceManufacturer: " + DeviceStatus.DeviceManufacturer + Environment.NewLine;
                    //name += "DeviceName: " + DeviceStatus.DeviceName + Environment.NewLine;
                    //name += "DeviceTotalMemory: " + DeviceStatus.DeviceTotalMemory + Environment.NewLine;
                    //name += "IsKeyboardDeployed: " + DeviceStatus.IsKeyboardDeployed + Environment.NewLine;
                    //name += "IsKeyboardPresent: " + DeviceStatus.IsKeyboardPresent + Environment.NewLine;
                    //name += "PowerSource: " + DeviceStatus.PowerSource + Environment.NewLine;
                    //name += Environment.NewLine;
                    //name += "ApplicationCurrentMemoryUsage: " + DeviceExtendedProperties.GetValue("ApplicationCurrentMemoryUsage") + Environment.NewLine;
                    //name += "ApplicationPeakMemoryUsage: " + DeviceExtendedProperties.GetValue("ApplicationPeakMemoryUsage") + Environment.NewLine;
                    //name += "ApplicationWorkingSetLimit: " + DeviceExtendedProperties.GetValue("ApplicationWorkingSetLimit") + Environment.NewLine;
                    //name += "DeviceFirmwareVersion: " + DeviceExtendedProperties.GetValue("DeviceFirmwareVersion") + Environment.NewLine;
                    //name += "DeviceHardwareVersion: " + DeviceExtendedProperties.GetValue("DeviceHardwareVersion") + Environment.NewLine;
                    //name += "DeviceManufacturer: " + DeviceExtendedProperties.GetValue("DeviceManufacturer") + Environment.NewLine;
                    //name += "DeviceName: " + DeviceExtendedProperties.GetValue("DeviceName") + Environment.NewLine;
                    //name += "DeviceTotalMemory: " + DeviceExtendedProperties.GetValue("DeviceTotalMemory") + Environment.NewLine;
                    //name += "DeviceUniqueId: " + Convert.ToBase64String(DeviceExtendedProperties.GetValue("DeviceUniqueId") as byte[]) + Environment.NewLine;
                    //name += "OS Version: " + Environment.OSVersion + Environment.NewLine;
                    //name += ".NEt Version: " + Environment.Version + Environment.NewLine;       
                    ////name += "IsApplicationPreinstalled: " + DeviceExtendedProperties.GetValue("IsApplicationPreinstalled");
                    ////name += "OriginalMobileOperatorName: " + DeviceExtendedProperties.GetValue("OriginalMobileOperatorName");
                    ////name += "PhysicalScreenResolution: " + DeviceExtendedProperties.GetValue("PhysicalScreenResolution");
                    ////name += "RawDpiX: " + DeviceExtendedProperties.GetValue("RawDpiX");
                    ////name += "RawDpiY: " + DeviceExtendedProperties.GetValue("RawDpiY");
                    
                    ////DispatcherTimer

                    

                    //MessageBox.Show(name);

    //                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
    //foreach (ManagementObject os in searcher.Get())
    //{
    //    result = os["Caption"].ToString();
    //    break;
    //}

                    break;
            }
        }

        private void Finance_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            UIElement = e.OriginalSource as FrameworkElement;
            PanoramaItem = UIElement.DataContext as PanoramaItemModel;

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
            UIElement = e.OriginalSource as FrameworkElement;
            PanoramaItem = UIElement.DataContext as PanoramaItemModel;

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