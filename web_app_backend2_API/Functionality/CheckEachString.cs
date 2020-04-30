using System;
using System.Collections.Generic;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API
{
    public class CheckEachString : ICheckEachString
    {
        public void CheckString(string[] line,List<string> line1,List<string>line2,List<string>line3)
        {
            if (line.Length > 0)
            {
                foreach (var i in line)
                {
                    if (i.Contains('x')) //Adds all the 'x' coordinates to a list of strings 
                    {
                        line1.Add(i);
                    }
                    else if (i.Contains('y') && !i.Contains('c')) //Adds all the 'y' coordinates to a list of strings
                    {
                        line2.Add(i);
                    }
                    else if (i.Contains('h') && i.Contains('e') && i.Contains('a') && i.Contains('t')
                    ) //Adds all the 'heatmapID' values to a list of strings
                    {
                        line3.Add(i);
                    }
                }

                System.Diagnostics.Debug.WriteLine("Elements sorted into lists");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No elements in line to check for");
            }
        }
    }
}