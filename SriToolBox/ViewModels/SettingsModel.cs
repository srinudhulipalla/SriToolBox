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
using System.IO.IsolatedStorage;

namespace SriToolBox.ViewModels
{
    public class CalcTaxSettings
    {
        IsolatedStorageSettings taxSettings;

        public double TaxExemption
        {
            get
            {
                return GetValue<double>("TaxExemption", 200000);
            }
            set
            {
                if (SetValue("TaxExemption", value))
                {
                    Save();
                }
            }
        }

        public CalcTaxSettings()
        {
            taxSettings = IsolatedStorageSettings.ApplicationSettings;
        }

        bool SetValue(string key, object value)
        {
            bool isValueSet = false;

            if (taxSettings.Contains(key))
            {
                if (taxSettings[key] != value)
                {
                    taxSettings[key] = value;
                    isValueSet = true;
                }
            }
            else
            {
                taxSettings.Add(key, value);
                isValueSet = true;
            }

            return isValueSet;
        }

        ValueType GetValue<ValueType>(string key, ValueType defaultValue)
        {
            ValueType returnValue;

            if (taxSettings.Contains(key))
            {
                returnValue = (ValueType)taxSettings[key];
            }
            else
            {
                returnValue = defaultValue;
            }

            return returnValue;
        }

        void Save()
        {
            taxSettings.Save();
        }

    }
}
