using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Battleship.Server.Services;

namespace Battleship.Server
{
    public class ConsoleServer
    {
        public static void Main()
        {
            try
            {
                // Create the ServiceHost.
                using (var host = new ServiceHost(typeof(ServerService)))
                {
                    host.Open();
                    Console.WriteLine("Press <Enter> to stop the service.");
                    Console.ReadLine();

                    // Close the ServiceHost.
                    host.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.Read();
            }
        }
    }
}
