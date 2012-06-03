using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using KinectUs.Json;
using Microsoft.Kinect;
namespace KinectUs.ConsoleClient
{
    public class JsonGestureManager
    {
        public class HandData
        {
            public DateTime Time { get; set; }
            public SkeletonPoint Position { get; set; }
            public Joint Joint { get; set; }
        }

        public JsonGestureManager()
        {
            Skeletons=new Subject<Skeleton>();

            //Wire up to right hand
            Skeletons
                .Where(x=> x.TrackingState == SkeletonTrackingState.Tracked)
                .Select(x=> new HandData {Time = DateTime.Now, Position = x.Position, Joint = x.Joints.First(j=> j.JointType == JointType.HandRight)})
                .Subscribe(OnNextRightHand, exception => Console.WriteLine(exception.Message));
        }

        private void OnNextRightHand(HandData obj)
        {
            

        }


        public Subject<Skeleton> Skeletons { get; set; }

         public void AddMessage(string message)
         {
             var skeleton = message.ToSkeleton();
             Skeletons.OnNext(skeleton);
         }
    }
}