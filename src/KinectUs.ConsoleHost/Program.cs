using System;
using KinectUs.ConsoleHost.Properties;
using KinectUs.Publisher;
using ZMQ;

namespace KinectUs.ConsoleHost
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var settings = Settings.Default;

            using (var kinectManager = new KinectManager())
            {
                var publisherConfiguration = new PublisherConfiguration
                                                 {
                                                     JsonPublishPort = settings.ZeroMQJsonPublishPort,
                                                     NumberOfThreads = settings.NumberOfZeroMQThreads,
                                                     Transport = Transport.TCP,
                                                     ProtoBufPublishPort = settings.ZeroMQProtoBufPublishPort
                                                 };


                using (IKinectPublisher publisher = new KinectPublisher(kinectManager, publisherConfiguration))
                {
                    publisher.Start();
                    Console.WriteLine("Press Enter to exit....");
                    Console.ReadLine();
                }
            }
        }
    }
}