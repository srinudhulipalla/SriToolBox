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
using System.Threading;
using System.Windows.Threading;

namespace SriToolBox.Device
{
    public partial class SystemInfo : PhoneApplicationPage
    {
        DeviceModel _deviceDetails;
        DispatcherTimer dispatchTimer = new DispatcherTimer();

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

            //bind data context
            this.DataContext = this.DeviceDetails;
            this.Loaded += new RoutedEventHandler(SystemInfo_Loaded);

            //register events
            DeviceStatus.PowerSourceChanged += new EventHandler(DeviceStatus_PowerSourceChanged);
            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(DeviceNetworkInformation_NetworkAvailabilityChanged);

            //dispatcher timer
            dispatchTimer.Interval = TimeSpan.FromSeconds(1);
            dispatchTimer.Tick += new EventHandler(dispatchTimer_Tick);
            dispatchTimer.Start();
        }

        protected void SystemInfo_Loaded(object sender, RoutedEventArgs e)
        {

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
                this.DeviceDetails.LoadMiscellaneous();
            });
        }

        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {                
                this.DeviceDetails.LoadNetwork();
            });
        }

        void dispatchTimer_Tick(object sender, EventArgs e)
        {
            PivotItem pItem = rootPivot.SelectedItem as PivotItem;
            if (pItem == null) return;

            switch (pItem.Name)
            {
                case "misc":                    
                    this.DeviceDetails.LoadMiscellaneous();
                    break;
            }            
        }


    }
}