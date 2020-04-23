using System;
using System.Collections.Generic;
using web_app_backend2.Models;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API
{
    public class CompareElements : ICompareElements
    {
        public void CompareHeatmapValues(List<Heatmap> items,int index)
        {
            if (index > 0)
            {
                for (int i = 0; i < index; i++)
                {
                    for (int j = 0; j < index; j++)
                    {
                        if (items[i].x == items[j].x && items[i].HeatmapID != items[j].HeatmapID &&
                            items[i].y == items[j].y) //compares two instances of the same list of heatmaps, and check if any coordinates match in the two list.
                        {
                            Console.WriteLine("It's a match! " + "\t ID: " + items[i].HeatmapID + " X: " + items[i].x +
                                              " Y: " + items[i].y + "\t ID: " + items[j].HeatmapID + " X: " +
                                              items[j].x + " Y: " + items[j].y);
                            items[i].Value++; //Increase the heatmap value by 1 where to sets of coordinates overlap
                        }
                        else
                        {
                            //Console.WriteLine("Not a match");
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine("Elements compared");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No elements to compare");
            }
        }
    }
}