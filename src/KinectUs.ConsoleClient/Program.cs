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

            var settings = ConsoleClient.Properties.Settings.Default;

            using (var context = new Context(1))
            {
                using (Socket subscriber = context.Socket(SocketType.SUB), sync = context.Socket(SocketType.PUSH))
                {
                    //  Connect our subscriber socket
                    subscriber.StringToIdentity("KinectClient" + Guid.NewGuid().ToString(), Encoding.Unicode);
                    subscriber.Subscribe("", Encoding.Unicode);
                    subscriber.Connect(Transport.TCP, "localhost", (uint) settings.ZeroMQSubscribePort);
                    
                   // sync.Connect(Transport.TCP, "localhost", (uint)settings.ZeroMQSubscribePort);
                   // sync.Send("", Encoding.Unicode);

                    string message = "";
                    while (!message.Equals("END"))
                    {
                        message = subscriber.Recv(Encoding.Unicode);
                        Console.WriteLine(message);
                    }
                }
            }
        }
    }
}
