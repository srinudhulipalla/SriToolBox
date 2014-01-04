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

namespace SriToolBox.ViewModels
{
    public class DeviceModel 
    {
        public DeviceModel()
        {
            this.System = new ObservableCollection<BindItemModel>();
            this.Memory = new ObservableCollection<BindItemModel>();
            this.Network = new ObservableCollection<BindItemModel>();

            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(DeviceNetworkInformation_NetworkAvailabilityChanged);
        }

        

        public ObservableCollection<BindItemModel> System { get; private set; }
        public ObservableCollection<BindItemModel> Memory { get; private set; }
        public ObservableCollection<BindItemModel> Network { get; private set; }

        

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

            this.IsDataLoaded = true;            
        }

        void LoadSystemData()
        {
            this.System.Add(new BindItemModel() { Title = "Manufacturer", Content = DeviceStatus.DeviceManufacturer });
            this.System.Add(new BindItemModel() { Title = "Model", Content = DeviceStatus.DeviceName });
            this.System.Add(new BindItemModel() { Title = "Firmware Version", Content = DeviceStatus.DeviceFirmwareVersion });
            this.System.Add(new BindItemModel() { Title = "Hardware Version", Content = DeviceStatus.DeviceHardwareVersion });            
            this.System.Add(new BindItemModel() { Title = "OS Version", Content = "Windows Phone " + Environment.OSVersion.Version });
            this.System.Add(new BindItemModel() { Title = ".NET CLR Version", Content = Environment.Version.ToString() });
            this.System.Add(new BindItemModel() { Title = "Unique Id", Content = Convert.ToBase64String(DeviceExtendedProperties.GetValue("DeviceUniqueId") as byte[]) });
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

           
            

            
        }

        void LoadNetworkData()
        {
                 
            NetworkInterfaceList list = new NetworkInterfaceList();

            this.Network.Add(new BindItemModel() { Title = "Operator", Content = DeviceNetworkInformation.CellularMobileOperator });
            this.Network.Add(new BindItemModel() { Title = "Network Availability", Content = DeviceNetworkInformation.IsNetworkAvailable.ToString() });
            this.Network.Add(new BindItemModel() { Title = "WiFi", Content = DeviceNetworkInformation.IsWiFiEnabled.ToString() });
            this.Network.Add(new BindItemModel() { Title = "Data Enabled", Content = DeviceNetworkInformation.IsCellularDataEnabled.ToString() });
            this.Network.Add(new BindItemModel() { Title = "Data Roaming Enabled", Content = DeviceNetworkInformation.IsCellularDataRoamingEnabled.ToString() });

            while (list.MoveNext())
            {
                NetworkInterfaceInfo networkInfo = list.Current;

                this.Network.Add(new BindItemModel() { Title = "Name", Content = networkInfo.InterfaceName });
                this.Network.Add(new BindItemModel() { Title = "Type", Content = networkInfo.InterfaceType.ToString() });
                this.Network.Add(new BindItemModel() { Title = "SubType", Content = networkInfo.InterfaceSubtype.ToString() });
                this.Network.Add(new BindItemModel() { Title = "State", Content = networkInfo.InterfaceState.ToString() });
                this.Network.Add(new BindItemModel() { Title = "Description", Content = networkInfo.Description });
                this.Network.Add(new BindItemModel() { Title = "Speed", Content = networkInfo.Bandwidth.ToString() });
                this.Network.Add(new BindItemModel() { Title = "Characteristics", Content = networkInfo.Characteristics.ToString() });
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
