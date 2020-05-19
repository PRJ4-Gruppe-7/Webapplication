using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_app_backend2.Models;
using web_app_backend2_API.Functionality;
using web_app_backend2_API.Interfaces;

namespace web_app_backend2_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeatmapController : ControllerBase
    {
        private static readonly int items = 8; //based on number of commas in original heatmap DB
        private static IHttpHandler _httpHandler;
        private static HttpClient HeatClient;
        private static HttpClient RefClient;
        private readonly List<string> LineX;
        private readonly List<string> LineY;
        private readonly List<string> RefLineX;
        private readonly List<string> RefLineY;
        private readonly List<string> LineHeat;
        private readonly List<string> LineRef;
        private readonly List<Heatmap> Heatmaps;
        private readonly List<Heatmap> HeatmapsRef;
        private readonly IExtractor Extractor;
        private readonly ICompareElements CompareElements;
        private readonly ICompareLists CompareList;
        private readonly ICheckEachString CheckEachString;


        public HeatmapController()
        {
            _httpHandler = new HttpHandler();
            HeatClient = new HttpClient();
            RefClient = new HttpClient();
            LineX = new List<string>();
            LineY = new List<string>();
            RefLineX = new List<string>();
            RefLineY = new List<string>();
            LineHeat = new List<string>();
            LineRef = new List<string>();
            Heatmaps = new List<Heatmap>();
            HeatmapsRef = new List<Heatmap>();
            Extractor = new Extractor();
            CompareElements = new CompareElements();
            CompareList = new CompareLists();
            CheckEachString = new CheckEachString();
        }

        [HttpGet]
        public async Task<IEnumerable<Heatmap>> Get()
        {

            GetAllReferenceid();
            GetAllHeatmapAsync();
            //DeleteAllHeatmapAsync();
            CompareList.CompareList(Heatmaps, HeatmapsRef);


            return HeatmapsRef;
        }


        public async Task<IEnumerable<Heatmap>> GetAllHeatmapAsync()
        {
           
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HeatClient.DefaultRequestHeaders.Add("ApiKey", "829320-adajdasd-12vasdas-baslk3"); //Server side API Token

                var handler = _httpHandler.Get("https://fruitflywebapi.azurewebsites.net/api/Heatmap", HeatClient).EnsureSuccessStatusCode();

                var responseBody = await handler.Content.ReadAsStringAsync();

                //Splits the responsebody into each datatype and it's value. Items * 1000 to make sure that a thousand heatmapID's can be loaded.
                var _line = responseBody.Split(',', items * 1000);

                //Checks for 3 specific strings that contain: 'x', 'y' and ('h', 'e', 'a' and 't').
                CheckEachString.CheckStringHeatmap(_line, LineX, LineY, LineHeat);

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
                CompareElements.CompareHeatmapValues(Heatmaps);

                //foreach (var i in Heatmaps)
                //{
                //    Console.WriteLine("X: " + i.x + "\t" + "Y: " + i.y + "\t" + "Value: " + i.Value + "\t" + "ID: " +
                //                      i.HeatmapID + "\n");
                //}

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            //DeleteAllHeatmapAsync();

            //returns all data stored in the list of heatmaps to the GET method
            Console.WriteLine("All heatmap data stored");
            return Heatmaps;
        }

        public async Task<IEnumerable<Heatmap>> GetAllReferenceid()
        {
            try
            {
                RefClient.DefaultRequestHeaders.Add("ApiKey", "829320-adajdasd-12vasdas-baslk3"); //Server side API Token

                var handler = _httpHandler.Get("https://fruitflywebapi.azurewebsites.net/api/Referencepoint", RefClient).EnsureSuccessStatusCode();

                var responseBody = await handler.Content.ReadAsStringAsync();

                //Splits the responsebody into each datatype and it's value. Items * 1000 to make sure that a thousand heatmapID's can be loaded.
                var _line = responseBody.Split(',', items * 1000);

                //Checks for 3 specific strings that contain: 'x', 'y' and ('h', 'e', 'a' and 't').
                CheckEachString.CheckStringReference(_line, RefLineX, RefLineY, LineRef);

                for (int i = 0; i < LineRef.Count; i++)
                {
                    //Here a new object of heatmap is created for each heatmap ID in the database. The values are collected from the sepereate list of x, y and heatmapID.
                    var obj = new Heatmap
                    {
                        x = Extractor.ExtractFromString(RefLineX[i]),
                        y = Extractor.ExtractFromString(RefLineY[i]),
                        HeatmapID = Extractor.ExtractFromString(LineRef[i]),
                        Value = 0
                    };

                    //adds all the added heatmaps into a list of heatmaps. 
                    HeatmapsRef.Add(obj);
                }


                //foreach (var i in HeatmapsRef)
                //{
                //    Console.WriteLine("X: " + i.x + "\t" + "Y: " + i.y + "\t" + "Value: " + i.Value + "\t" + "ID: " +
                //                      i.HeatmapID + "\n");
                //}

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            //returns all data stored in the list of heatmaps to the GET method
            Console.WriteLine("All Reference data stored");
            return HeatmapsRef;
        }


        public async void DeleteAllHeatmapAsync()
        {
            try
            {

                //Deleting
                var response = await HeatClient.DeleteAsync("https://fruitflywebapi.azurewebsites.net/api/Heatmap");
                response.EnsureSuccessStatusCode();
                Console.WriteLine("All heatmap data on DB removed");

                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("delete failed");
                Console.WriteLine("Exception message: {0} ", ex.Message);
            }
        }

    }
}


