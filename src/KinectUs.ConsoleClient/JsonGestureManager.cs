using System;
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
        private RightHandGesture _rightHandGesture;

        public JsonGestureManager()
        {
            Skeletons = new Subject<Skeleton>();
            _rightHandGesture = new RightHandGesture(Skeletons);

        }

        public Subject<Skeleton> Skeletons { get; set; }

         public void AddMessage(string message)
         {
             var skeleton = message.ToSkeletons();
             if(skeleton.Any())
             {
                 Skeletons.OnNext(skeleton.First());
             }
             
         }
    }
}