using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using Microsoft.Kinect;
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

                   var throttled = manager.Skeletons
                       .Throttle(TimeSpan.FromSeconds(3));

                    throttled.Subscribe(x =>
                        {
                            Console.WriteLine(x.TrackingId);

                        });


                    var observable = Observable.Interval(TimeSpan.FromMilliseconds(750)).TimeInterval();

                    using (observable.Subscribe(
                        x => Console.WriteLine("{0}: {1}", x.Value, x.Interval)))
                    {
                        Console.WriteLine("Press any key to unsubscribe");
                        Console.ReadKey();
                    }

                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();



                    Console.WriteLine("Connected to publisher");

                    string message = "";
                    while (!message.Equals("END"))
                    {
                        message = jsonSubscriber.Recv(Encoding.Unicode);
                        manager.AddMessage(message); 
#if DEBUG
                        //Console.WriteLine(message);
#endif
                    }
                }
            }
        }
    }
}
