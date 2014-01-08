using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace SriToolBox
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Device = new ObservableCollection<PanoramaItemModel>();
            this.Finance = new ObservableCollection<PanoramaItemModel>();
            this.Others = new ObservableCollection<PanoramaItemModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<PanoramaItemModel> Device { get; private set; }
        public ObservableCollection<PanoramaItemModel> Finance { get; private set; }
        public ObservableCollection<PanoramaItemModel> Others { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            //Device data
            this.Device.Add(new PanoramaItemModel() { ID = 1, ImagePath = "Images/Device/SystemInfo.png", Name = "System Info", Description = "know more about your device" });
            this.Device.Add(new PanoramaItemModel() { ID = 2, ImagePath = "Images/Device/CallHistory.png", Name = "Call History", Description = "check your call records" });
            this.Device.Add(new PanoramaItemModel() { ID = 3, ImagePath = "Images/Device/MessageHistory.png", Name = "Message History", Description = "see your message history" });
            this.Device.Add(new PanoramaItemModel() { ID = 4, ImagePath = "Images/Device/Multimedia.png", Name = "Multimedia", Description = "get details about your media" });
            this.Device.Add(new PanoramaItemModel() { ID = 5, ImagePath = "Images/Device/DataUsage.png", Name = "Data Usage", Description = "track your data usage" });
            this.Device.Add(new PanoramaItemModel() { ID = 6, ImagePath = "Images/Device/Apps.png", Name = "Apps", Description = "review apps with its size" });

            //Finance data
            this.Finance.Add(new PanoramaItemModel() { ID = 1, ImagePath = "Images/Finance/CalcInterest.png", Name = "Calc Interest", Description = "calculate the interest to be earn" });
            this.Finance.Add(new PanoramaItemModel() { ID = 2, ImagePath = "Images/Finance/CalcTax.png", Name = "Calculate Tax", Description = "check tax liability on TDS" });

            //Settings data
            this.Others.Add(new PanoramaItemModel() { ID = 1, ImagePath = "Images/Others/About.png", Name = "About", Description = "know more about SriToolBox" });
            this.Others.Add(new PanoramaItemModel() { ID = 2, ImagePath = "Images/Others/Feedback.png", Name = "Feedback", Description = "tell us your feedback about app" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}