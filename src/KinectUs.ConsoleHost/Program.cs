using System;
using KinectUs.ConsoleHost.Properties;
using KinectUs.Publisher;

namespace KinectUs.ConsoleHost
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var settings = Settings.Default;

            using (var kinectManager = new KinectManager())
            {
                using (IKinectPublisher publisher = new KinectPublisher(kinectManager, settings.ZeroMQPublishPort, settings.ZeroMqTransport, settings.NumberOfZeroMQThreads, settings.ZeroMqPullPort))
                {
                    publisher.Start();
                    Console.WriteLine("Press Enter to exit....");
                    Console.ReadLine();
                }
            }
        }
    }
}