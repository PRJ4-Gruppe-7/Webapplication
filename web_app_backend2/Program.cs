﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using web_app_backend2.Models;

namespace web_app_backend2
{
    class Program
    {
        private static int items = 9;
        static HttpClient client = new HttpClient();
        private static string[] line;
        private static List<string> lineX = new List<string>();
        private static List<string> lineY = new List<string>();
        private static List<string> lineHeat = new List<string>();
        private static List<Heatmap> heatmaps = new List<Heatmap>();

        static async Task Main()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                client.DefaultRequestHeaders.Add("ApiKey", "829320-adajdasd-12vasdas-baslk3");

                HttpResponseMessage response = await client.GetAsync("https://fruitflyapi.azurewebsites.net/api/Heatmap");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);


                //Splits the responsebody into each datatype and it's value. Items * 1000 to make sure that a thousand heatmapID's can be loaded.
                line = responseBody.Split(',', items * 1000);

                //Checks for 3 specific strings that contain: 'x', 'y' and ('h', 'e', 'a' and 't').
                CheckEachString();


                for (int i = 0; i < lineHeat.Count; i++)
                {
                    var obj = new Heatmap
                    {
                        x = ExtractInteger(lineX[i]),
                        y = ExtractInteger(lineY[i]),
                        HeatmapID = ExtractInteger(lineHeat[i]),
                        Value = 1
                    };

                    heatmaps.Add(obj);
                }

                foreach (var i in heatmaps)
                {
                    Console.WriteLine("X: "+ i.x + "\t" + "Y: "+ i.y + "\t" + "Value: " + i.Value + "\t" + "ID: " + i.HeatmapID + "\n");
                }

                /*
                Console.WriteLine("This is the lineX, lineY and lineHeat\n\n");
                for (int i = 0; i < lineHeat.Count; i++)
                {
                    Console.Write(lineX[i] + "\t" + lineY[i] + "\t" + lineHeat[i] + "\t" + "\n\n");
                }*/

                CompareElements(heatmaps);

                JsonSerialize(heatmaps);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            
        }

        public static void JsonSerialize(List<Heatmap> dataHeatmaps)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dataHeatmaps);    //Serializes the object into json format.
            //Console.WriteLine(json + "\n");

            //location of data file found in /bin/debug/netcoreapp3.1/
            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "heat.txt");


            using (StreamWriter outputFile = new StreamWriter(xslLocation))
            {
                outputFile.Write(json);
            }
        }

        public static int ExtractInteger(string lines)
        {
            string a = lines;
            string b = string.Empty;
            int val = 0;

            for (int j = 0; j < a.Length; j++)
            {
                if (Char.IsDigit(a[j]))
                    b += a[j];
            }

            if (b.Length > 0)
                val = int.Parse(b);
            return val;
        }

        public static void CheckEachString()
        {
            foreach (var i in line)
            {
                if (i.Contains('x'))
                {
                    lineX.Add(i);
                }
                else if (i.Contains('y') && !i.Contains('c'))
                {
                    lineY.Add(i);
                }
                else if (i.Contains('h') && i.Contains('e') && i.Contains('a') && i.Contains('t'))
                {
                    lineHeat.Add(i);
                }
            }
        }

        public static void CompareElements(List<Heatmap> items)
        {
            for (int i = 0; i < lineHeat.Count; i++)
            {
                for (int j = 0; j < lineHeat.Count; j++)
                {
                    if (items[i].x == items[j].x && items[i].HeatmapID != items[j].HeatmapID && items[i].y == items[j].y)
                    {
                        Console.WriteLine("It's a match! " + "\t ID: "+ items[i].HeatmapID + " X: " + items[i].x + " Y: " + items[i].y + "\t ID: " + items[j].HeatmapID + " X: " + items[j].x + " Y: " + items[j].y);
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