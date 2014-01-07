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
using System.Windows.Threading;

namespace SriToolBox.ViewModels
{
    public class DeviceModel 
    {
        //System Info
        public ObservableCollection<BindItemModel> WPSystem { get; private set; }
        public ObservableCollection<BindItemModel> Memory { get; private set; }
        public ObservableCollection<BindItemModel> Network { get; private set; }
        public ObservableCollection<BindItemModel> Miscellaneous { get; private set; }

        //Multimedia
        public ObservableCollection<BindItemModel> Multimedia { get; private set; }
        public ObservableCollection<BindItemModel> Player { get; private set; }

        public DeviceModel()
        {
            this.WPSystem = new ObservableCollection<BindItemModel>();
            this.Memory = new ObservableCollection<BindItemModel>();
            this.Network = new ObservableCollection<BindItemModel>();
            this.Miscellaneous = new ObservableCollection<BindItemModel>();
            this.Multimedia = new ObservableCollection<BindItemModel>();
            this.Player = new ObservableCollection<BindItemModel>();
        } 

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadSystemData()
        {
            this.LoadSystem();
            this.LoadMemory();
            this.LoadNetwork();
            this.LoadMiscellaneous();

            this.IsDataLoaded = true;            
        }

        void LoadSystem()
        {
            object liveId = null;

            try
            {
                UserExtendedProperties.TryGetValue("ANID", out liveId);

                if (liveId != null)
                {
                    liveId = liveId.ToString().Substring(2, 32);
                    liveId = Guid.Parse(liveId.ToString());
                }
            }
            catch { liveId = "N/A"; }

            this.WPSystem.Add(new BindItemModel() { Title = "Manufacturer", Content = DeviceStatus.DeviceManufacturer });
            this.WPSystem.Add(new BindItemModel() { Title = "Model", Content = DeviceStatus.DeviceName });
            this.WPSystem.Add(new BindItemModel() { Title = "Firmware Version", Content = DeviceStatus.DeviceFirmwareVersion });
            this.WPSystem.Add(new BindItemModel() { Title = "Hardware Version", Content = DeviceStatus.DeviceHardwareVersion });
            this.WPSystem.Add(new BindItemModel() { Title = "OS Version", Content = System.Environment.OSVersion.ToString() });
            this.WPSystem.Add(new BindItemModel() { Title = ".NET CLR Version", Content = System.Environment.Version.ToString() });
            this.WPSystem.Add(new BindItemModel() { Title = "Device Id", Content = Convert.ToBase64String(DeviceExtendedProperties.GetValue("DeviceUniqueId") as byte[]) });
            this.WPSystem.Add(new BindItemModel() { Title = "Live Id", Content = (liveId == null) ? "N/A" : liveId.ToString() });
        }

        void LoadMemory()
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

        void LoadNetwork()
        {
            StringBuilder sb = new StringBuilder();

            string internet = DeviceNetworkInformation.IsNetworkAvailable ? "Yes" : "No";
            string wiFi = DeviceNetworkInformation.IsWiFiEnabled ? "Yes" : "No";
            string data = DeviceNetworkInformation.IsCellularDataEnabled ? "Yes" : "No";
            string dataRoaming = DeviceNetworkInformation.IsCellularDataRoamingEnabled ? "Yes" : "No"; 

            this.Network.Add(new BindItemModel() { Title = "Operator", Content = DeviceNetworkInformation.CellularMobileOperator });
            this.Network.Add(new BindItemModel() { Title = "Internet Availability", Content = internet });
            this.Network.Add(new BindItemModel() { Title = "WiFi", Content = wiFi });
            this.Network.Add(new BindItemModel() { Title = "Data Enabled", Content = data });
            this.Network.Add(new BindItemModel() { Title = "Data Roaming Enabled", Content = dataRoaming });            

            foreach (NetworkInterfaceInfo networkInfo in new NetworkInterfaceList())
            {
                if (networkInfo.InterfaceName.ToLower().StartsWith("software loopback")) continue;

                sb.AppendLine("Name:            " + networkInfo.InterfaceName);
                sb.AppendLine("Type:              " + networkInfo.InterfaceType);
                sb.AppendLine("SubType:        " + networkInfo.InterfaceSubtype);
                sb.AppendLine("State:              " + networkInfo.InterfaceState);
                sb.AppendLine("Bandwidth:     " + networkInfo.Bandwidth / 1000 + " Mbps");
                sb.AppendLine("Description:    " + networkInfo.Description);

                sb.AppendLine();
            }

            this.Network.Add(new BindItemModel() { Title = "Connected To", Content = sb.ToString() });
        }

        void LoadMiscellaneous()
        {
            string powerSource = DeviceStatus.PowerSource == PowerSource.Battery ? "Running on Battery" : "Battery is charging";
            string frontCamera = PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing) ? "Yes" : "No";
            string primaryCamera = PhotoCamera.IsCameraTypeSupported(CameraType.Primary) ? "Yes" : "No";            

            this.Miscellaneous.Add(new BindItemModel() { Title = "Power Status", Content = powerSource });
            this.Miscellaneous.Add(new BindItemModel() { Title = "Front Camera", Content = frontCamera });
            this.Miscellaneous.Add(new BindItemModel() { Title = "Primary Camera", Content = primaryCamera });

            try
            {
                string radioRegion = FMRadio.Instance.CurrentRegion.ToString();
                this.Miscellaneous.Add(new BindItemModel() { Title = "Radio Support", Content = "Yes" });
                this.Miscellaneous.Add(new BindItemModel() { Title = "Radio Region", Content = radioRegion });
            }
            catch (RadioDisabledException)
            {
                this.Miscellaneous.Add(new BindItemModel() { Title = "Radio Support", Content = "No" });
            }
        }

        public void LoadMultimediaData()
        {
            this.LoadMedia();
            this.LoadPlayer();

            this.IsDataLoaded = true;
        }

        void LoadMedia()
        {
            MediaLibrary library = new MediaLibrary();
            this.Multimedia.Add(new BindItemModel() { Title = "Photos", Content = library.Pictures.Count.ToString() });
            this.Multimedia.Add(new BindItemModel() { Title = "Songs", Content = library.Songs.Count.ToString() });
            this.Multimedia.Add(new BindItemModel() { Title = "Albums", Content = library.Albums.Count.ToString() });
            this.Multimedia.Add(new BindItemModel() { Title = "Artists", Content = library.Artists.Count.ToString() });
            this.Multimedia.Add(new BindItemModel() { Title = "Genres", Content = library.Genres.Count.ToString() });
            this.Multimedia.Add(new BindItemModel() { Title = "Playlists", Content = library.Playlists.Count.ToString() });
        }

        void LoadPlayer()
        {
            string runningSoneName = "N/A";

            if (MediaPlayer.State == MediaState.Playing)
            {
                runningSoneName = MediaPlayer.Queue.ActiveSong.Name;
            }

            this.Player.Add(new BindItemModel() { Title = "Player Status", Content = MediaPlayer.State.ToString() });
            this.Player.Add(new BindItemModel() { Title = "Currently Playing Song", Content = runningSoneName });
        }

        public void ClearSystemData()
        {
            this.WPSystem.Clear();
            this.Memory.Clear();
            this.Network.Clear();
            this.Miscellaneous.Clear();
            IsDataLoaded = false;
        }

        public void ClearMultimediaData()
        {
            this.Multimedia.Clear();
            this.Player.Clear();
            IsDataLoaded = false;
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
        
    }
}
