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
using SriToolBox.ViewModels;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;

namespace SriToolBox.Device
{
    public partial class SystemInfo : PhoneApplicationPage
    {
        DeviceModel _deviceDetails;

        DeviceModel DeviceDetails
        {
            get
            {
                if (_deviceDetails == null)
                    _deviceDetails = new DeviceModel();

                return _deviceDetails;
            }
        }

        public SystemInfo()
        {
            InitializeComponent();

            this.DataContext = this.DeviceDetails;
            this.Loaded += new RoutedEventHandler(SystemInfo_Loaded);
            DeviceStatus.PowerSourceChanged += new EventHandler(DeviceStatus_PowerSourceChanged);
            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(DeviceNetworkInformation_NetworkAvailabilityChanged);
        }        

        protected void SystemInfo_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.DeviceDetails.IsDataLoaded)
            {
                //this.DeviceDetails.LoadSystemData();
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem pItem = e.AddedItems[0] as PivotItem;

            switch (pItem.Name)
            {
                case "system": 
                    if (this.DeviceDetails.WPSystem.Count == 0) this.DeviceDetails.LoadSystem();
                    break;
                case "memory": 
                    if (this.DeviceDetails.Memory.Count == 0) this.DeviceDetails.LoadMemory();
                    break;
                case "network": 
                    if (this.DeviceDetails.Network.Count == 0) this.DeviceDetails.LoadNetwork();
                    break;
                case "misc": 
                    if (this.DeviceDetails.Miscellaneous.Count == 0) this.DeviceDetails.LoadMiscellaneous();
                    break;
            }
        }

        void DeviceStatus_PowerSourceChanged(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.DeviceDetails.Miscellaneous.Clear();
                this.DeviceDetails.LoadMiscellaneous();               
            });
        }

        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.DeviceDetails.Network.Clear();
                this.DeviceDetails.LoadNetwork();
            });
        }        

    }
}