using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using KinectUs.Core;
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

                    var throttled = manager.Skeletons;
                       //.Throttle(TimeSpan.FromSeconds(3));

                    var joints = manager
                        .Skeletons
                        .SelectMany(x=> x.Joints);

                    var leftAndRightHands =
                        joints.Where(j => j.JointType == JointType.HandLeft || j.JointType == JointType.HandRight);

                    throttled.Subscribe(skeleton =>
                        {
                            Console.WriteLine("LeftHand " + skeleton.LeftHand().Position.X);
                            Console.WriteLine("Right Hand" + skeleton.RightHand().Position.X);
                        });

                    manager.Skeletons
                        .Where(x => x.LeftHand().Position.X > x.RightHand().Position.X)
                        .Subscribe(skeleton =>
                            {
                                Console.WriteLine("The left hand is Higher than the right");
                            })
                        ;
                         

                    
                    Console.WriteLine("Connected to publisher");

                    string message = "";
                    while (!message.Equals("END"))
                    {
                        message = jsonSubscriber.Recv(Encoding.Unicode);
                        manager.AddMessage(message); 
#if DEBUG
                       // Console.WriteLine(message);
#endif
                    }
                }
            }
        }
    }
}
