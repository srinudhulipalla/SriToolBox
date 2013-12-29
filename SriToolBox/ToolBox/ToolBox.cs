using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SriToolBox.ToolBox
{
    public class CompoundInterest
    {
        private double _amount;
        private double _interestRate;
        private double _duration;
        private DurationValue _durationMethod;
        private FrequencyValue _frequency;
        private double _interestEarned;
        private double _maturityAmount;
        private string _lastError;

        public enum DurationValue
        {
            Day = 365,
            Month = 12,
            Year = 1
        }

        public enum FrequencyValue
        {
            Monthly = 12,
            Quarterly = 4,
            HalfYearly = 2,
            Yearly = 1
        }

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public double InterestRate
        {
            get { return _interestRate; }
            set { _interestRate = value; }
        }

        public double Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public DurationValue DurationMethod
        {
            get { return _durationMethod; }
            set { _durationMethod = value; }
        }

        public FrequencyValue Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        public double InterestEarned
        {
            get { return _interestEarned; }
        }

        public double MaturityAmount
        {
            get { return _maturityAmount; }
        }

        public string LastError
        {
            get { return _lastError; }            
        }

        public bool Calculate()
        {
            try
            {
                double value1 = 1 + ((_interestRate / 100) / (int)_frequency);
                double value2 = (int)_frequency * (_duration / (int)_durationMethod);
                double value3 = Math.Pow(value1, value2);

                _maturityAmount = Math.Round(_amount * value3, 2);
                _interestEarned = Math.Round(_maturityAmount - _amount, 2);
                
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

    }
}
