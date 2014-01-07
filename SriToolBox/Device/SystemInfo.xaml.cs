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
                this.DeviceDetails.LoadSystemData();
            }
        }

        void DeviceStatus_PowerSourceChanged(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.DeviceDetails.ClearSystemData();
                this.DeviceDetails.LoadSystemData();                
            });
        }

        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.DeviceDetails.ClearSystemData();
                this.DeviceDetails.LoadSystemData();
            });
        }

    }
}