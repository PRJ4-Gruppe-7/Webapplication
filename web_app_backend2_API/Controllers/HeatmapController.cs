using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_app_backend2.Models;

namespace web_app_backend2_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeatmapController : ControllerBase
    {
        private static readonly int items = 9; //based on number of commas in original heatmap DB
        static readonly HttpClient Client = new HttpClient();
        private static string[] _line;
        private static readonly List<string> LineX = new List<string>();
        private static readonly List<string> LineY = new List<string>();
        private static readonly List<string> LineHeat = new List<string>();
        private static readonly List<Heatmap> Heatmaps = new List<Heatmap>();
        private static readonly Extractor Extractor = new Extractor();
        private static readonly CompareElements CompareElements = new CompareElements();
        private static readonly CheckEachString CheckEachString = new CheckEachString();



        [HttpGet]
        public async Task<IEnumerable<Heatmap>> Get()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                Client.DefaultRequestHeaders.Add("ApiKey", "829320-adajdasd-12vasdas-baslk3"); //Server side API Token

                HttpResponseMessage response =
                    await Client.GetAsync("https://fruitflyapi.azurewebsites.net/api/Heatmap"); //Heatmap API Address
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                //Splits the responsebody into each datatype and it's value. Items * 1000 to make sure that a thousand heatmapID's can be loaded.
                _line = responseBody.Split(',', items * 1000);

                //Checks for 3 specific strings that contain: 'x', 'y' and ('h', 'e', 'a' and 't').
                CheckEachString.CheckString(_line, LineX, LineY, LineHeat);

                for (int i = 0; i < LineHeat.Count; i++)
                {
                    //Here a new object of heatmap is created for each heatmap ID in the database. The values are collected from the sepereate list of x, y and heatmapID.
                    var obj = new Heatmap
                    {
                        x = Extractor.ExtractFromString(LineX[i]),
                        y = Extractor.ExtractFromString(LineY[i]),
                        HeatmapID = Extractor.ExtractFromString(LineHeat[i]),
                        Value = 1
                    };

                    //adds all the added heatmaps into a list of heatmaps. 
                    Heatmaps.Add(obj);
                }

                //compares if there are more than 1 element with the same x and y coordinates. If so, the value of the item will increase with 1 pr matching elements.
                CompareElements.CompareHeatmapValues(Heatmaps, LineHeat.Count);

                foreach (var i in Heatmaps)
                {
                    Console.WriteLine("X: " + i.x + "\t" + "Y: " + i.y + "\t" + "Value: " + i.Value + "\t" + "ID: " +
                                      i.HeatmapID + "\n");
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            //returns all data stored in the list of heatmaps to the GET method
            return Heatmaps;
        }

        [HttpDelete]
        public async void Delete()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                Client.DefaultRequestHeaders.Add("ApiKey", "829320-adajdasd-12vasdas-baslk3"); //Server side API Token

                HttpResponseMessage response =
                    await Client.DeleteAsync("https://fruitflyapi.azurewebsites.net/api/Heatmap"); //Heatmap API Address

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}

