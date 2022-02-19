using System;
using System.Drawing;
using System.Globalization;

namespace PowerDocu.Common
{
    public static class ColourHelper
    {
        public static string ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim().ToLower();

            if (cssColor.StartsWith("#"))
            {
                return ColorToHex(ColorTranslator.FromHtml(cssColor));
            }
            else if (cssColor.StartsWith("rgb")) //rgb or argb
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    return null;
                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');
                if (parts.Length >= 3)
                {
                    int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                    int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                    int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                    if (parts.Length == 3)
                    {
                        return ColorToHex(Color.FromArgb(r, g, b));
                    }
                    else if (parts.Length == 4)
                    {
                        float a;
                        if (parts[3].Contains("%"))
                        {
                            a = float.Parse(parts[3].Replace("%", ""), CultureInfo.InvariantCulture) / 100;
                        }
                        else
                        {
                            a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                        }
                        return ColorToHex(Color.FromArgb((int)(a * 255), r, g, b));
                    }
                }
            }
            return null;
        }

        public static string ColorToHex(Color color)
        {
            return ColorTranslator.ToHtml(Color.FromArgb(color.ToArgb()));
        }

        public static bool IsValidColorString(string cssColor)
        {
            cssColor = cssColor.Trim().ToLower();
            if (cssColor.StartsWith("#") && cssColor.Length <= 7) return true;
            if (cssColor.StartsWith("rgb"))
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                    return false;
                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');
                if (parts.Length >= 3)
                {
                    //todo additional validation?
                    return true;
                }
            }
            return false;
        }
    }
}