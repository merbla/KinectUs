using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace KinectUs.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new JsonGestureManager();
            var settings = ConsoleClient.Properties.Settings.Default;

            using (var context = new Context(1))
            {
                using (Socket jsonSubscriber = context.Socket(SocketType.SUB), sync = context.Socket(SocketType.PUSH))
                {
                    jsonSubscriber.StringToIdentity("KinectClient" + Guid.NewGuid().ToString(), Encoding.Unicode);
                    jsonSubscriber.Subscribe("", Encoding.Unicode);
                    jsonSubscriber.Connect(Transport.TCP, "localhost", (uint) settings.ZeroMQJsonSubscribePort);
                    
                    //TODO: Sync to get send settings etc. data to be published
                   // sync.Connect(Transport.TCP, "localhost", (uint)settings.ZeroMQSubscribePort);
                   // sync.Send("", Encoding.Unicode);

                    Console.WriteLine("Connected to publisher");

                    string message = "";
                    while (!message.Equals("END"))
                    {
                        message = jsonSubscriber.Recv(Encoding.Unicode);
                        manager.AddMessage(message); 
#if DEBUG
                        Console.WriteLine(message);
#endif
                    }
                }
            }
        }
    }
}
