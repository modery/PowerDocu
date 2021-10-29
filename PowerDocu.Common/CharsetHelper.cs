using System;
using System.Globalization;
using System.Text;

namespace PowerDocu.Common
{
    public class CharsetHelper
    {

        // this function cleans up names a lot. Replaces Umlauts and similar with "safe letters" (e.g. ä to a), and strips most other characters that would cause errors (e.g. Chinese chracters)
        // Problems are mostly happening in the graphviz library. Not sure how much control we have and what other options there are, considering this a temporary fix for the moment
        public static string GetSafeName(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return "NameNotDefined";
            }
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var ascii = new ASCIIEncoding();
            byte[] encodedBytes = ascii.GetBytes(stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace(":", "-"));
            var cleaned = ascii.GetString(encodedBytes).Replace("?", "-");
            return cleaned;
        }

    }
}
