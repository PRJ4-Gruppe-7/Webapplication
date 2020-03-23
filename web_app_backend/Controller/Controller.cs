using System;
using System.IO;
using System.Reflection;
using Microsoft.Data.SqlClient;
using web_app_backend.Models;


namespace web_app_backend
{
    public class Controller
    {

        //server connection string
        private static string constr =
            "Server = tcp:fruitflyserver.database.windows.net,1433;Initial Catalog = FruitFly; Persist Security Info=False;User ID = dalleman; Password=Frugtflue1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        private static SqlConnection cnn = new SqlConnection(constr);       //Sql connector that opens and closes the connection

        const string query1 = "SELECT * FROM dbo.Referencepoints;";         //Query string that reads all content from dbo.Referencepoints

        public void OpenConnection()
        {
            cnn.Open();                                                     //opens the connection to the sqlServer - must be called at the beginning of main
        }

        public void CloseConnection()
        {
            cnn.Close();                                                    //Closes the connection to the sqlServer - must be called at the end of main
        }


        public void Receive()
        {
            SqlCommand cmdHeatmap = new SqlCommand(query1, cnn);      //sqlCommand that lets you call query on the sqlconnector
            SqlDataReader Reader = cmdHeatmap.ExecuteReader();              //Data reader that reads forward only data from the sqldatabase table

            if (Reader.HasRows)                                             //If statement that checks if the table has data or not
            {
                while (Reader.Read())
                {
                    //display retrieved record (first column only/string value)
                    Console.WriteLine("ID : " + Reader.GetInt32(0) + " |Cat : " + Reader.GetInt32(1) + " |RSS1 : " +
                                      Reader.GetInt32(2) + " |RSSI2 : " + Reader.GetInt32(3) + " |RSSI3 : " + Reader.GetInt32(4)
                                      + " |X : " + Reader.GetInt32(5) + " |Y : " + Reader.GetInt32(6));

                    int X = Reader.GetInt32(1);     //Stores value of int32 on second spot (x) on index
                    int Y = Reader.GetInt32(2);     //Stores value of int32 on third spot (y) on index

                    var obj = new Heatmap()
                    {
                        x = X,
                        y = Y
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);    //Serializes the object into json format.
                    Console.WriteLine(json);

                    //location of data file found in /bin/debug/netcoreapp3.1/
                    string executableLocation = Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location);
                    string xslLocation = Path.Combine(executableLocation, "heat.txt");


                    // Write the string array to a new file named "WriteLines.txt".
                    using (StreamWriter outputFile = new StreamWriter(xslLocation))
                    {
                        outputFile.WriteLine(json);
                    }

                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

        }

        public void Insert()
        {
            
        }

        public void set()
        {
            
        }
    }
}