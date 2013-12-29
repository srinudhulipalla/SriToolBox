﻿using System;
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

namespace SriToolBox
{
    public partial class MainPage : PhoneApplicationPage
    {
        FrameworkElement PanoramaElement = null;
        ItemViewModel ModelItem = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void ToolBox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PanoramaElement = e.OriginalSource as FrameworkElement;
            ModelItem = PanoramaElement.DataContext as ItemViewModel;

            switch (ModelItem.ID)
            {
                case 1: this.NavigationService.Navigate(new Uri("/ToolBox/CalcInterest.xaml", UriKind.Relative));
                    break;
                case 2: MessageBox.Show("Calc Tax");
                    break;
            }     
        }

        private void Settings_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PanoramaElement = e.OriginalSource as FrameworkElement;
            ModelItem = PanoramaElement.DataContext as ItemViewModel;

            switch (ModelItem.ID)
            {
                case 1: MessageBox.Show("About");
                    break;
                case 2: MessageBox.Show("Feedback");
                    break;
            }            
        }

        
    }
}