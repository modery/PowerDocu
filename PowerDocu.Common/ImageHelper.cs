using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PowerDocu.Common
{

    public class ImageHelper
    {
        public static void ConvertImageTo32(string imagepath, string destinationpath)
        {
            Bitmap bmp = new Bitmap(imagepath);
            Console.WriteLine((int)(32 * (bmp.Height / bmp.Width)));
            Bitmap resized = new Bitmap(bmp, new Size(32, (int)(32 * (bmp.Height / bmp.Width))));
            resized.Save(destinationpath, ImageFormat.Png);
            resized.Dispose();
            bmp.Dispose();

        }

        public static string GetBase64(string filepath)
        {
            if (File.Exists(filepath))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
                return Convert.ToBase64String(imageArray);
            }
            return "";
        }

    }

}