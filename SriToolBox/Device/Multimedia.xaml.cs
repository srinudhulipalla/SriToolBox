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
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace SriToolBox.Device
{
    public partial class Multimedia : PhoneApplicationPage, IApplicationService
    {
        DispatcherTimer frameworkDispatcherTimer;
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

        public Multimedia()
        {
            InitializeComponent();

            this.DataContext = this.DeviceDetails;
            this.Loaded += new RoutedEventHandler(Multimedia_Loaded);
            MediaPlayer.MediaStateChanged += new EventHandler<EventArgs>(MediaPlayer_MediaStateChanged);

            this.frameworkDispatcherTimer = new DispatcherTimer();
            this.frameworkDispatcherTimer.Interval = TimeSpan.FromTicks(333333);
            this.frameworkDispatcherTimer.Tick += frameworkDispatcherTimer_Tick;
            FrameworkDispatcher.Update();            
        }  

        void Multimedia_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.DeviceDetails.IsDataLoaded)
            {
                this.DeviceDetails.LoadMultimediaData();
            }
        }

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            this.DeviceDetails.ClearMultimediaData();
            this.DeviceDetails.LoadMultimediaData();
        }

        void frameworkDispatcherTimer_Tick(object sender, EventArgs e) { FrameworkDispatcher.Update(); }
        void IApplicationService.StartService(ApplicationServiceContext context) { this.frameworkDispatcherTimer.Start(); }
        void IApplicationService.StopService() { this.frameworkDispatcherTimer.Stop(); }

    }
}