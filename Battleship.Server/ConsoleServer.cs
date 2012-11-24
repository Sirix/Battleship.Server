using System;
using System.ServiceModel;
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
                    ServerService.T("Started!");
                    Console.WriteLine("[{0}] Battleship Server is started", DateTime.Now.ToLongTimeString());
                    Console.WriteLine("Work at {0}", host.BaseAddresses[0]);
                   
                    Console.WriteLine("Press <Enter> to stop the service.");
                    Console.ReadLine();

                    // Close the ServiceHost.
                    host.Close();
                }
            }
            catch(CommunicationObjectFaultedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Please start this app under Administrator Account or consider using [netsh] utility");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.Read();
        }
    }
}