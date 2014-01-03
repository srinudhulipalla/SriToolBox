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

namespace SriToolBox.ViewModels
{
    public class DeviceModel 
    {
        public DeviceModel()
        {
            this.System = new ObservableCollection<BindItemModel>();
        }

        public ObservableCollection<BindItemModel> System { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            this.LoadSystemInfo();

            this.IsDataLoaded = true;            
        }

        void LoadSystemInfo()
        {
            this.System.Add(new BindItemModel() { Title = "Manufacturer", Content = DeviceStatus.DeviceManufacturer });
            this.System.Add(new BindItemModel() { Title = "Model", Content = DeviceStatus.DeviceName });
            this.System.Add(new BindItemModel() { Title = "Hardware Version", Content = DeviceStatus.DeviceHardwareVersion });
            this.System.Add(new BindItemModel() { Title = "Firmware Version", Content = DeviceStatus.DeviceFirmwareVersion });
            this.System.Add(new BindItemModel() { Title = "OS Version", Content = "Windows Phone " + Environment.OSVersion.Version });
            this.System.Add(new BindItemModel() { Title = ".NET CLR Version", Content = Environment.Version.ToString() });
            this.System.Add(new BindItemModel() { Title = "UniqueId", Content = Convert.ToBase64String(DeviceExtendedProperties.GetValue("DeviceUniqueId") as byte[]) });
        }

        
    }
}
