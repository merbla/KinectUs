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
        KinectStatus Start();
        Subject<Skeleton[]> Skeletons { get; }
    }

    public class KinectManager : IKinectManager
    {
        private KinectSensorCollection _sensors;
        private KinectSensor _currentSensor;

        public KinectManager()
        {
            Skeletons = new Subject<Skeleton[]>();
        }

        public Subject<Skeleton[]> Skeletons { get; private set; }

        public void Dispose()
        {
            _sensors.ToList().ForEach(s => s.Dispose());
        }

        public KinectStatus Start()
        {
            _sensors = KinectSensor.KinectSensors;
            if (!_sensors.Any(x => x.Status == KinectStatus.Connected))
            {
                return KinectStatus.NotReady;
            }
            _sensors.ToList().ForEach(s=>
                                          {
                                              
                                              s.SkeletonStream.Enable();
                                              //s.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                                              s.SkeletonFrameReady += (sender, args) => AddToObservableSkeletons(args);

                                             

                                              s.Start();
                                          });

            return KinectStatus.Connected;
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