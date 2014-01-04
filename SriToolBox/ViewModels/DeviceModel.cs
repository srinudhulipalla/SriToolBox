using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SriToolBox.ViewModels;
using Microsoft.Phone.Info;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone.Net.NetworkInformation;
using System.Text;
using Microsoft.Devices;
using Microsoft.Devices.Radio;
using System.Windows.Navigation;

namespace SriToolBox.ViewModels
{
    public class DeviceModel 
    {
        public DeviceModel()
        {
            this.WPSystem = new ObservableCollection<BindItemModel>();
            this.Memory = new ObservableCollection<BindItemModel>();
            this.Network = new ObservableCollection<BindItemModel>();
            this.Miscellaneous = new ObservableCollection<BindItemModel>();

            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(DeviceNetworkInformation_NetworkAvailabilityChanged);
        }        

        public ObservableCollection<BindItemModel> WPSystem { get; private set; }
        public ObservableCollection<BindItemModel> Memory { get; private set; }
        public ObservableCollection<BindItemModel> Network { get; private set; }
        public ObservableCollection<BindItemModel> Miscellaneous { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            this.LoadSystemData();
            this.LoadMemoryData();
            this.LoadNetworkData();
            this.LoadMiscellaneousData();

            this.IsDataLoaded = true;            
        }

        void LoadSystemData()
        {
            this.WPSystem.Add(new BindItemModel() { Title = "Manufacturer", Content = DeviceStatus.DeviceManufacturer });
            this.WPSystem.Add(new BindItemModel() { Title = "Model", Content = DeviceStatus.DeviceName });
            this.WPSystem.Add(new BindItemModel() { Title = "Firmware Version", Content = DeviceStatus.DeviceFirmwareVersion });
            this.WPSystem.Add(new BindItemModel() { Title = "Hardware Version", Content = DeviceStatus.DeviceHardwareVersion });
            this.WPSystem.Add(new BindItemModel() { Title = "OS Version", Content = "Windows Phone " + System.Environment.OSVersion.Version });
            this.WPSystem.Add(new BindItemModel() { Title = ".NET CLR Version", Content = System.Environment.Version.ToString() });
            this.WPSystem.Add(new BindItemModel() { Title = "Unique Id", Content = Convert.ToBase64String(DeviceExtendedProperties.GetValue("DeviceUniqueId") as byte[]) });
        }

        void LoadMemoryData()
        {
            this.Memory.Add(new BindItemModel() { Title = "Total Memory", Content = GetDiskSize(DeviceStatus.DeviceTotalMemory) });

            string ss = Microsoft.Devices.Environment.DeviceType.ToString();
            //ss = Microsoft.Devices.MediaHistory.Instance.NowPlaying.Title;
            bool b = Microsoft.Devices.PhotoCamera.IsCameraTypeSupported(Microsoft.Devices.CameraType.FrontFacing);
            ss = Microsoft.Devices.Radio.FMRadio.Instance.PowerMode.ToString();
            ss = Microsoft.Devices.Radio.FMRadio.Instance.CurrentRegion.ToString();

            ss = Microsoft.Phone.Info.DeviceStatus.PowerSource.ToString();
            b = Microsoft.Phone.Info.MediaCapabilities.IsMultiResolutionVideoSupported;
            //ss = Microsoft.Phone.Info.UserExtendedProperties.GetValue("ANID").ToString();
            //Microsoft.Phone.UserData.Account a = new Microsoft.Phone.UserData.Account();
            //ss = a.Kind.ToString();
            //ss = a.Name;
            Microsoft.Phone.UserData.Contacts cc = new Microsoft.Phone.UserData.Contacts();
            MediaLibrary lib = new MediaLibrary();
            int i = lib.Pictures.Count;

            //Geolocator
        }

        void LoadNetworkData()
        {
                 
            NetworkInterfaceList list = new NetworkInterfaceList();

            this.Network.Add(new BindItemModel() { Title = "Operator", Content = DeviceNetworkInformation.CellularMobileOperator });
            this.Network.Add(new BindItemModel() { Title = "Network Availability", Content = DeviceNetworkInformation.IsNetworkAvailable.ToString() });
            this.Network.Add(new BindItemModel() { Title = "WiFi", Content = DeviceNetworkInformation.IsWiFiEnabled.ToString() });
            this.Network.Add(new BindItemModel() { Title = "Data Enabled", Content = DeviceNetworkInformation.IsCellularDataEnabled.ToString() });
            this.Network.Add(new BindItemModel() { Title = "Data Roaming Enabled", Content = DeviceNetworkInformation.IsCellularDataRoamingEnabled.ToString() });

            StringBuilder sb = new StringBuilder();

            while (list.MoveNext())
            {
                NetworkInterfaceInfo networkInfo = list.Current;

                sb.AppendLine("Name:            " + networkInfo.InterfaceName);
                sb.AppendLine("Type:            " + networkInfo.InterfaceType);
                sb.AppendLine("SubType:         " + networkInfo.InterfaceSubtype);
                sb.AppendLine("State:           " + networkInfo.InterfaceState);
                sb.AppendLine("Description:     " + networkInfo.Description);
                sb.AppendLine("Speed:           " + networkInfo.Bandwidth);
                sb.AppendLine("Characteristics: " + networkInfo.Characteristics);
                sb.AppendLine();
            }

            this.Network.Add(new BindItemModel() { Title = "Connection List", Content = sb.ToString() });
        }

        void LoadMiscellaneousData()
        {
            string frontCamera = "Yes", primaryCamera = "Yes";

            if (!PhotoCamera.IsCameraTypeSupported(Microsoft.Devices.CameraType.FrontFacing)) frontCamera = "No";
            if (!PhotoCamera.IsCameraTypeSupported(Microsoft.Devices.CameraType.Primary)) primaryCamera = "No";

            this.Miscellaneous.Add(new BindItemModel() { Title = "Front Camera", Content = frontCamera });
            this.Miscellaneous.Add(new BindItemModel() { Title = "Primary Camera", Content = primaryCamera });

            try
            {
                string radioRegion = FMRadio.Instance.CurrentRegion.ToString();
                this.Miscellaneous.Add(new BindItemModel() { Title = "Radio Support", Content = "Yes" });
                this.Miscellaneous.Add(new BindItemModel() { Title = "Radio Region", Content = radioRegion });
            }
            catch (RadioDisabledException ex)
            {
                this.Miscellaneous.Add(new BindItemModel() { Title = "Radio Support", Content = "No" });
            }
        }

        string GetDiskSize(long sizeInBytes)
        {
            double length = (double)sizeInBytes;
            string[] size = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int i = 0;

            while (Math.Round(length / 1024) >= 1)
            {
                length /= 1024;
                i++;
            }

            return string.Format("{0:0.##} {1}", length, size[i]);
        }

        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            this.Network.Clear();
            this.LoadNetworkData();
        }

        
    }
}
