using System;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration.Install;
using System.Text;

//http://www.codeproject.com/KB/dotnet/WinSvcSelfInstaller.aspx
namespace MTC.Host
{
    public static class SelfInstaller
    {
        private static readonly string _exePath = Assembly.GetExecutingAssembly().Location;
        public static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { _exePath, "-s" });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", _exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
