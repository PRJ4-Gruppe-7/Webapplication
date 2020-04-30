namespace web_app_backend
{
    class Program : Controller
    {
        static void Main(string[] args)
        {
            IController Control = new Controller();

            bool running = true;

            while (running)
            {
                Control.OpenConnection();
                Control.Receive();
                Control.CloseConnection();
            }
        }
    }
}
