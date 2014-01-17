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
using System.IO.IsolatedStorage;

namespace SriToolBox.ViewModels
{
    public class DeviceModel 
    {
        //System Info
        public ObservableCollection<BindItemModel> WPSystem { get; private set; }
        public ObservableCollection<BindItemModel> Memory { get; private set; }
        public ObservableCollection<BindItemModel> Network { get; private set; }
        public ObservableCollection<BindItemModel> Power { get; private set; }
        public ObservableCollection<BindItemModel> Camera { get; private set; }
        public ObservableCollection<BindItemModel> Radio { get; private set; }
        public ObservableCollection<BindItemModel> Others { get; private set; }
        public ObservableCollection<BindItemModel> Miscellaneous { get; private set; }

        //Multimedia
        public ObservableCollection<BindItemModel> Multimedia { get; private set; }
        public ObservableCollection<BindItemModel> Player { get; private set; }

        public DeviceModel()
        {
            this.WPSystem = new ObservableCollection<BindItemModel>();
            this.Memory = new ObservableCollection<BindItemModel>();
            this.Network = new ObservableCollection<BindItemModel>();
            this.Power = new ObservableCollection<BindItemModel>();
            this.Camera = new ObservableCollection<BindItemModel>();
            this.Radio = new ObservableCollection<BindItemModel>();
            this.Others = new ObservableCollection<BindItemModel>();
            this.Miscellaneous = new ObservableCollection<BindItemModel>();
            this.Multimedia = new ObservableCollection<BindItemModel>();
            this.Player = new ObservableCollection<BindItemModel>();
        } 

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadSystemInfo()
        {
            this.LoadSystem();
            this.LoadMemory();
            this.LoadNetwork();
            this.LoadMiscellaneous();
            this.IsDataLoaded = true;            
        }

        public void LoadMultimediaData()
        {
            this.LoadMedia();
            this.LoadPlayer();
            this.IsDataLoaded = true;
        }

        public void LoadSystem()
        {
            this.WPSystem.Clear();
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

        public void LoadMemory()
        {
            this.Memory.Clear();
            this.Memory.Add(new BindItemModel() { Title = "Total Memory", Content = Common.GetDiskSize(DeviceStatus.DeviceTotalMemory) });

            using (var file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string freeSpace = Common.GetDiskSize(file.AvailableFreeSpace);
                this.Memory.Add(new BindItemModel() { Title = "Available Free Space", Content = freeSpace });
            }

            


            //System.Device.Location.GeoCoordinate g = new System.Device.Location.GeoCoordinate();
            
            //Microsoft.Phone.UserData.Contacts cc = new Microsoft.Phone.UserData.Contacts();
            
            //Geolocator
        }

        public void LoadNetwork()
        {
            this.Network.Clear();
            StringBuilder sb = new StringBuilder();

            string internet = DeviceNetworkInformation.IsNetworkAvailable ? "Yes" : "No";
            string wiFi = DeviceNetworkInformation.IsWiFiEnabled ? "On" : "Off";
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

            this.Network.Add(new BindItemModel() { Title = "Connected To", Content = string.IsNullOrEmpty(sb.ToString()) ? "None" : sb.ToString() });
        }

        public void LoadMiscellaneous()
        {
            LoadPower();
            LoadCamera();
            LoadRadio();
            LoadOthers();
        }

        public void LoadPower()
        {
            this.Power.Clear();

            string powerSource = DeviceStatus.PowerSource == PowerSource.Battery ? "Running on Battery" : "Battery is charging";
            this.Power.Add(new BindItemModel() { Title = "Power Status", Content = powerSource });
        }

        public void LoadCamera()
        {
            this.Camera.Clear();

            string frontCamera = PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing) ? "Yes" : "No";
            string primaryCamera = PhotoCamera.IsCameraTypeSupported(CameraType.Primary) ? "Yes" : "No";

            this.Camera.Add(new BindItemModel() { Title = "Front Camera", Content = frontCamera });
            this.Camera.Add(new BindItemModel() { Title = "Primary Camera", Content = primaryCamera });
        }

        public void LoadRadio()
        {
            this.Radio.Clear();

            try
            {
                string radioRegion = FMRadio.Instance.CurrentRegion.ToString();
                this.Radio.Add(new BindItemModel() { Title = "Radio Support", Content = "Yes" });
                this.Radio.Add(new BindItemModel() { Title = "Radio Region", Content = radioRegion });
            }
            catch (RadioDisabledException)
            {
                this.Radio.Add(new BindItemModel() { Title = "Radio Support", Content = "No" });
            }
        }

        public void LoadOthers()
        {
            this.Others.Clear();

            this.Others.Add(new BindItemModel() { Title = "Bootup Time", Content = Common.GetTimeFromTicks(System.Environment.TickCount) });
            this.Others.Add(new BindItemModel() { Title = "Resolution", Content = Application.Current.Host.Content.ActualWidth + " x " + Application.Current.Host.Content.ActualHeight });
        }
        

        void LoadMedia()
        {
            this.Multimedia.Clear();
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
            this.Player.Clear();
            string runningSoneName = "N/A";

            if (MediaPlayer.State == MediaState.Playing)
            {
                runningSoneName = MediaPlayer.Queue.ActiveSong.Name;
            }

            this.Player.Add(new BindItemModel() { Title = "Player Status", Content = MediaPlayer.State.ToString() });
            this.Player.Add(new BindItemModel() { Title = "Currently Playing Song", Content = runningSoneName });
        }  
               
        
    }
}
