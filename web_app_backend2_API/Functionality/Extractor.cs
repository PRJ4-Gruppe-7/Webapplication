using System;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API
{
    public class Extractor : IExtractor
    {

        public int ExtractFromString(string lines)
        {
            //Checks if any element in the chosen string contains any numbers. These number are then stored in the empty string and parsed to an int.
            string a = lines;
            string b = string.Empty;
            int val = 0;

            for (int j = 0; j < a.Length; j++)
            {
                if (Char.IsDigit(a[j]))
                {
                    b += a[j];
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("String don't contain any digits");    //Prints if the string don't contain digits
                }
            }

            if (b.Length > 0)
                val = int.Parse(b);

            System.Diagnostics.Debug.WriteLine("Digits extracted from string");

            return val;
        }
    }
}