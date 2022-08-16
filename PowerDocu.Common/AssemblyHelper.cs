using System;
using System.IO;
using System.Reflection;

namespace PowerDocu.Common
{
    public static class AssemblyHelper
    {
        //this method only worked while the app was not self-contained. Now it is not advised to use it, as it returns the path to where dotnet extracts the files to (temporary directory).
        //Recommended to use GetExcecutablePath() instead
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string GetExecutablePath()
        {
            var mainModule = System.Diagnostics.Process.GetCurrentProcess().MainModule;
            return mainModule.FileName.Replace(mainModule.ModuleName, "");
        }
    }
}