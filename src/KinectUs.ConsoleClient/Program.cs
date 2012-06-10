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
                using (Socket subscriber = context.Socket(SocketType.SUB), sync = context.Socket(SocketType.PUSH))
                {
                    subscriber.StringToIdentity("KinectClient" + Guid.NewGuid().ToString(), Encoding.Unicode);
                    subscriber.Subscribe("", Encoding.Unicode);
                    subscriber.Connect(Transport.TCP, "localhost", (uint) settings.ZeroMQJsonSubscribePort);
                    
                   // sync.Connect(Transport.TCP, "localhost", (uint)settings.ZeroMQSubscribePort);
                   // sync.Send("", Encoding.Unicode);

                    Console.WriteLine("Connected to publisher");

                    string message = "";
                    while (!message.Equals("END"))
                    {
                        message = subscriber.Recv(Encoding.Unicode);
                        manager.AddMessage(message);
                        
                        //Console.WriteLine(message);
                    }
                }
            }
        }
    }
}
