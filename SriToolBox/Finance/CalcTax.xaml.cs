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

namespace SriToolBox.Finance
{
    public partial class CalcTax : PhoneApplicationPage
    {
        public CalcTax()
        {
            InitializeComponent();
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            SriToolBox.ViewModels.CalcTaxSettings ss = new ViewModels.CalcTaxSettings();
            this.NavigationService.Navigate(new Uri("/Finance/CalcTaxSettings.xaml", UriKind.Relative));

        }
    }
}