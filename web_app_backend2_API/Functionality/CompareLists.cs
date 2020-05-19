using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_app_backend2.Models;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API.Functionality
{
    public class CompareLists : ICompareLists
    {
        public void CompareList(List<Heatmap> heatmap, List<Heatmap> Reference)
        {
            if (heatmap.Count > 0)
            {
                foreach (var i in heatmap)
                {
                    foreach (var j in Reference)
                    {
                        if (i.x.Equals(j.x) && i.y.Equals(j.y))
                        {
                            j.Value ++;
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("Koordinates compared and value increased");

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No koordinates to compare");
            }
        }
    }
}
