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
using System.Reflection;

namespace SriToolBox
{
    public class Common
    {
        public static string GetAppVersion()
        {
            try
            {
                string[] assemblyParts = Assembly.GetExecutingAssembly().FullName.Split(',', '=');
                return assemblyParts[2];
            }
            catch { return "Unknown"; }            
        }

        public static string GetDiskSize(long sizeInBytes)
        {
            double length = (double)sizeInBytes;
            string[] size = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int i = 0;

            while (Math.Round(length / 1024) >= 1)
            {
                length /= 1024;
                i++;
            }

            return string.Format("{0:0.##} {1}", length, size[i]);
        }

        public static string GetTimeFromTicks(long tickCount)
        {
            string time = string.Empty;;

            TimeSpan ts = TimeSpan.FromMilliseconds(tickCount);

            if (ts.Days != 0) time = string.Format("{0} days ", ts.Days);
            if (ts.Hours != 0) time += string.Format("{0} hours ", ts.Hours);
            if (ts.Minutes != 0) time += string.Format("{0} minutes ", ts.Minutes);
            if (ts.Seconds != 0) time += string.Format("{0} seconds ", ts.Seconds);

            return time.Equals(string.Empty) ? "N/A" : time;
        }

    }
}
