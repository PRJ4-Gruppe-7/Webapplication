using System;
using System.IO;
using System.IO.Enumeration;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using web_app_backend.Models;


namespace web_app_backend
{
    class Program : Controller
    {

        static void Main(string[] args)
        {
            Controller Control = new Controller();

            Control.OpenConnection();
            Control.Receive();
            Control.CloseConnection();


        }

    }
}
