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
            this.Device = new ObservableCollection<ItemViewModel>();
            this.Finance = new ObservableCollection<ItemViewModel>();
            this.Others = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Device { get; private set; }
        public ObservableCollection<ItemViewModel> Finance { get; private set; }
        public ObservableCollection<ItemViewModel> Others { get; private set; }

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
            this.Device.Add(new ItemViewModel() { ID = 1, ImagePath = "Images/Finance/CalcInterest.png", Name = "System Info", Description = "know about your device" });
            this.Device.Add(new ItemViewModel() { ID = 2, ImagePath = "Images/Finance/CalcTax.png", Name = "Call History", Description = "check your call history" });             
            this.Device.Add(new ItemViewModel() { ID = 3, ImagePath = "Images/Finance/CalcTax.png", Name = "Message History", Description = "see your messages history" });
            this.Device.Add(new ItemViewModel() { ID = 4, ImagePath = "Images/Finance/CalcInterest.png", Name = "Multimedia", Description = "get details about your media" });
            this.Device.Add(new ItemViewModel() { ID = 5, ImagePath = "Images/Finance/CalcTax.png", Name = "Data Usage", Description = "track data usage on your device" });

            //Finance data
            this.Finance.Add(new ItemViewModel() { ID = 1, ImagePath = "Images/Finance/CalcInterest.png", Name = "Calc Interest", Description = "calculate the interest which you get on principal amount" });
            this.Finance.Add(new ItemViewModel() { ID = 2, ImagePath = "Images/Finance/CalcTax.png", Name = "Calculate Tax", Description = "calculate the tax payable over the taxable income" });

            //Settings data
            this.Others.Add(new ItemViewModel() { ID = 1, ImagePath = "Images/Others/About.png", Name = "About", Description = "want to know more about SriToolBox?" });
            this.Others.Add(new ItemViewModel() { ID = 2, ImagePath = "Images/Others/Feedback.png", Name = "Feedback", Description = "tell us your feedback about app" });

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