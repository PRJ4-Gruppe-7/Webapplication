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
