using System;
using System.Collections.Generic;
using web_app_backend2.Models;

namespace web_app_backend2_API
{
    public class CompareElements
    {
        public void CompareHeatmapValues(List<Heatmap> items,int index)
        {
            for (int i = 0; i < index; i++)
            {
                for (int j = 0; j < index; j++)
                {
                    if (items[i].x == items[j].x && items[i].HeatmapID != items[j].HeatmapID && items[i].y == items[j].y)
                    {
                        Console.WriteLine("It's a match! " + "\t ID: " + items[i].HeatmapID + " X: " + items[i].x + " Y: " + items[i].y + "\t ID: " + items[j].HeatmapID + " X: " + items[j].x + " Y: " + items[j].y);
                        items[i].Value++;
                    }
                    else
                    {
                        //Console.WriteLine("Not a match");
                    }
                }
            }
        }
    }
}