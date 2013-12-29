using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SriToolBox
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _imagePath;
        private string _Name;
        private string _Description;

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                if (value != _imagePath)
                {
                    _imagePath = value;
                    NotifyPropertyChanged("ImagePath");
                }
            }
        }

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        //private string _lineOne;
        ///// <summary>
        ///// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        ///// </summary>
        ///// <returns></returns>
        //public string LineOne
        //{
        //    get
        //    {
        //        return _lineOne;
        //    }
        //    set
        //    {
        //        if (value != _lineOne)
        //        {
        //            _lineOne = value;
        //            NotifyPropertyChanged("LineOne");
        //        }
        //    }
        //}

        //private string _lineTwo;
        ///// <summary>
        ///// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        ///// </summary>
        ///// <returns></returns>
        //public string LineTwo
        //{
        //    get
        //    {
        //        return _lineTwo;
        //    }
        //    set
        //    {
        //        if (value != _lineTwo)
        //        {
        //            _lineTwo = value;
        //            NotifyPropertyChanged("LineTwo");
        //        }
        //    }
        //}

        //private string _lineThree;
        ///// <summary>
        ///// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        ///// </summary>
        ///// <returns></returns>
        //public string LineThree
        //{
        //    get
        //    {
        //        return _lineThree;
        //    }
        //    set
        //    {
        //        if (value != _lineThree)
        //        {
        //            _lineThree = value;
        //            NotifyPropertyChanged("LineThree");
        //        }
        //    }
        //}

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