using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
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

                    //Here a new object of heatmap is created for each heatmap ID in the database. The values are collected from the sepereate list of x, y and heatmapID.
                    var obj = new Heatmap
                    {
                        x = ExtractInteger(lineX[i]),
                        y = ExtractInteger(lineY[i]),
                        HeatmapID = ExtractInteger(lineHeat[i]),
                        Value = 1
                    };

                    //adds all the added headmaps into a list of heatmaps. 
                    heatmaps.Add(obj);
                }

                //compares if there are more than 1 element with the same x and y coordinates. If so, the value of the item will increase with 1 pr matching elements.
                CompareElements(heatmaps);

                foreach (var i in heatmaps)
                {
                    Console.WriteLine("X: " + i.x + "\t" + "Y: " + i.y + "\t" + "Value: " + i.Value + "\t" + "ID: " + i.HeatmapID + "\n");
                }

                //serializes the list of heatmaps and 
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

            //writes the entire json file in terminal
            Console.WriteLine(json + "\n");

            //location of data file found in /bin/debug/netcoreapp3.1/
            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            string xslLocation = Path.Combine(executableLocation, "heat.json");  //stores json in a txt file named heat.

            if (File.Exists(xslLocation))
            {
                File.Delete(xslLocation);
                using (StreamWriter outputFile = new StreamWriter(xslLocation, true))
                {
                    outputFile.Write(json.ToString());
                    outputFile.Close();
                }
            }
            else if (!File.Exists(xslLocation))
            {
                using (StreamWriter outputFile = new StreamWriter(xslLocation, true))
                {
                    outputFile.Write(json.ToString());
                    outputFile.Close();
                }
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
