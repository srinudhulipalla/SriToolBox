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
using System.Text.RegularExpressions;

namespace SriToolBox.ToolBox
{
    public partial class CalcInterest : PhoneApplicationPage
    {
        public CalcInterest()
        {
            InitializeComponent();            
        }

        private void BtnCalculte_Click(object sender, RoutedEventArgs e)
        {
            string errorMsg = string.Empty;

            if (!ValidData(ref errorMsg))
            {
                MessageBox.Show(errorMsg, "Warning!", MessageBoxButton.OK);
                return;
            }

            CompoundInterest Interest = new CompoundInterest();
            Interest.Amount = double.Parse(txtAmount.Text);
            Interest.InterestRate = double.Parse(txtInterest.Text);
            Interest.Duration = double.Parse(txtDuration.Text);

            switch (lstDuration.SelectedIndex)
            {
                case 0: Interest.DurationMethod = CompoundInterest.DurationValue.Day;
                    break;
                case 1: Interest.DurationMethod = CompoundInterest.DurationValue.Month;
                    break;
                case 2: Interest.DurationMethod = CompoundInterest.DurationValue.Year;
                    break;
            }

            switch (lstFrequency.SelectedIndex)
            {
                case 0: Interest.Frequency = CompoundInterest.FrequencyValue.Monthly;
                    break;
                case 1: Interest.Frequency = CompoundInterest.FrequencyValue.Quarterly;
                    break;
                case 2: Interest.Frequency = CompoundInterest.FrequencyValue.HalfYearly;
                    break;
                case 3: Interest.Frequency = CompoundInterest.FrequencyValue.Yearly;
                    break;
            }

            if (Interest.Calculate())
            {
                string message = "Amount Invested Rs.: " + Interest.Amount + Environment.NewLine + Environment.NewLine;
                message += "Interest Earned Rs.: " + Interest.InterestEarned + Environment.NewLine;
                message += "Maturity Value Rs.: " + Interest.MaturityAmount;

                MessageBox.Show(message, "Done!", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(Interest.LastError, "Error!", MessageBoxButton.OK);
            }            
        }

        private void IconHome_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void IconClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = txtInterest.Text = txtDuration.Text = string.Empty;
            lstDuration.SelectedIndex = lstFrequency.SelectedIndex = 1;
        }

        bool ValidData(ref string errorMsg)
        {
            Regex isNumber = new Regex(@"^[0-9]+(\.[0-9]+)?$");

            if (!isNumber.IsMatch(txtAmount.Text))
                errorMsg = "Principal Amount entered is Invalid";

            else if (!isNumber.IsMatch(txtInterest.Text))
                errorMsg = "Rate of Interest entered is Invalid";

            else if (!isNumber.IsMatch(txtDuration.Text))
                errorMsg = "Duration entered is Invalid";

            return string.IsNullOrEmpty(errorMsg);
        }

        
    }
}