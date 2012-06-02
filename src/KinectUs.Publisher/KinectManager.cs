using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Kinect;

namespace KinectUs.Publisher
{
    public interface IKinectManager :IDisposable
    {
        bool Start();
        Subject<Skeleton[]> Skeletons { get; }
    }
    public class KinectManager : IKinectManager
    {
        private readonly KinectSensor _kinectSensor;
        private readonly KinectSensorCollection _sensors;
        private readonly KinectSensor _currentSensor;

        public KinectManager()
        {
            _sensors = KinectSensor.KinectSensors;
            if(!_sensors.Any(x=> x.Status == KinectStatus.Connected))
            {
                //Change to logger
                Console.WriteLine("No Kinected Connected!!");
                return;
            }

            //At this point assume there is only one sensor
            _currentSensor = _sensors.First(x=> x.Status == KinectStatus.Connected);

            Skeletons = new Subject<Skeleton[]>();
        }

        public Subject<Skeleton[]> Skeletons { get; private set; }

        public void Dispose()
        {
            _currentSensor.Dispose();
        }

        public bool Start()
        {
            if (_currentSensor == null)
                return false;
            _currentSensor.SkeletonStream.Enable();
            _currentSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
            _currentSensor.SkeletonFrameReady += (sender, args) => AddToObservableSkeletons(args);
            _currentSensor.Start();

            return true;
        }

        private void AddToObservableSkeletons(SkeletonFrameReadyEventArgs args)
        { 
            using (var skeletonFrame = args.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    var skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                    Skeletons.OnNext(skeletons);
                }
            }
        } 
    }



    //var eventAsObservable =
    //    Observable.FromEventPattern<SkeletonFrameReadyEventArgs>(_currentSensor, "SkeletonFrameReady")
    //        .Select(x => x.EventArgs)
    //        .AsObservable();
     

}