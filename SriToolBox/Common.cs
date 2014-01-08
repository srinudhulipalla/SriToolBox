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
            catch { return "X"; }            
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
    }
}
