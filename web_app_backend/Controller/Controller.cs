using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using web_app_backend.Models;


namespace web_app_backend
{
    public class Controller
    {
        List<int> Xcoor = new List<int>();
        List<int> Ycoor = new List<int>();


        //server connection string
        private static string constr =
            "Server = tcp:fruitflyserver.database.windows.net,1433;Initial Catalog = FruitFly; Persist Security Info=False;User ID = dalleman; Password=Frugtflue1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        private static SqlConnection Cnn = new SqlConnection(constr);       //Sql connector that opens and closes the connection

        //Query string that reads all content from dbo.Referencepoints
        const string Query1 = "SELECT X,Y, Referencepoints.HeatmapId, Heatmaps.Strength FROM Referencepoints INNER JOIN Heatmaps ON ReferencepointID = Heatmaps.HeatmapId";    
        

        public void OpenConnection()
        {
            Cnn.Open();                                                     //opens the connection to the sqlServer - must be called at the beginning of main
        }

        public void CloseConnection()
        {
            Cnn.Close();                                                    //Closes the connection to the sqlServer - must be called at the end of main
        }


        public void Receive()
        {
            SqlCommand cmdHeatmap = new SqlCommand(Query1,Cnn);      //sqlCommand that lets you call query on the sqlconnector
            SqlDataReader Reader = cmdHeatmap.ExecuteReader();              //Data reader that reads forward only data from the sqldatabase table
             List<Heatmap> heatmaps = new List<Heatmap>();

            if (Reader.HasRows)                                             //If statement that checks if the table has data or not
            {
                while (Reader.Read())
                {
                    //display retrieved record (first column only/string value)
                    Console.WriteLine("X : " + Reader.GetInt32(0) + " |Y : " + Reader.GetInt32(1) + " |HeatmapID : " +
                                      Reader.GetInt32(2) + " |Strength : " + Reader.GetFloat(3));

                    int X = Reader.GetInt32(0);       //Stores value of int32 on second spot (X) on index
                    int Y = Reader.GetInt32(1);       //Stores value of int32 on second spot (Y) on index
                    int ID = Reader.GetInt32(2);      //Stores value of int32 on second spot (ID) on index
                    float S = Reader.GetFloat(3);     //Stores value of Float on second spot (strength) on index


                    var obj = new Heatmap()
                    {
                        x = X,
                        y = Y,
                        HeatmapID = ID,
                        Strength = S
                    };

                    heatmaps.Add(obj);
                }

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(heatmaps);    //Serializes the object into json format.
                Console.WriteLine(json + "\n");

                //location of data file found in /bin/debug/netcoreapp3.1/
                string executableLocation = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location);
                string xslLocation = Path.Combine(executableLocation, "heat.txt");


                using (StreamWriter outputFile = new StreamWriter(xslLocation))
                {
                    outputFile.Write(json);
                }

            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }
    }
}