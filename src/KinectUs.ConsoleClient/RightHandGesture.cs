using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Kinect;

namespace KinectUs.ConsoleClient
{
    public class RightHandGesture
    {
        private readonly Subject<Skeleton> _skeletons;
        private Subject<HandData> _handData;
        public float SwipeMinimalLength { get; set; }
        public float SwipeMaximalHeight { get; set; }
        public int SwipeMininalDuration { get; set; }
        public int SwipeMaximalDuration { get; set; }

        public RightHandGesture(Subject<Skeleton> skeletons)
        {

            SwipeMinimalLength = 0.4f;
            SwipeMaximalHeight = 0.2f;
            SwipeMininalDuration =  250;
            SwipeMaximalDuration = 1500;
            _skeletons = skeletons;
            _handData = new Subject<HandData>();
            Data = new List<HandData>();

            _skeletons
                .Where(x=> x != null)
                //.Where(x => x.TrackingState == SkeletonTrackingState.Tracked)
                .Select(
                    x =>
                    new HandData
                        {
                            Time = DateTime.Now,
                            Position = x.Position,
                            Joint = x.Joints.First(j => j.JointType == JointType.HandRight)
                        })
                .Subscribe(OnNextRightHand, exception => Console.WriteLine(exception.Message));

            //_skeletons
            //    .Where(x=> x!=null)
            //    .Subscribe(skeleton => { Console.WriteLine("a");}, exception => { });
        }

        private void OnNextRightHand(HandData handData)
        {
            Data.Add(handData);
            //Console.WriteLine("New Hand Data");
            LookForGesture();
        }
        
        public List<HandData> Data { get; set; }

        private bool ScanPositions(Func<HandData, HandData, bool> heightFunction, Func<HandData, HandData, bool> directionFunction, Func<HandData, HandData, bool> lengthFunction, int minTime, int maxTime)
        {
            int start = 0;

            for (int index = 1; index < Data.Count - 1; index++)
            {
                if (!heightFunction(Data[0], Data[index]) || !directionFunction(Data[index], Data[index + 1]))
                {
                    start = index;
                }

                if (lengthFunction(Data[index], Data[start]))
                {
                    double totalMilliseconds = (Data[index].Time - Data[start].Time).TotalMilliseconds;
                    if (totalMilliseconds >= minTime && totalMilliseconds <= maxTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void DetectGesture()
        {
            HandData data = null;
            foreach (var handData in Data)
            {
                if(data ==null)
                {
                    data = handData;
                    break;
                    
                }
                else
                {
                   // bool height = Math.Abs(p2.Joint.Position.Y - p1.Joint.Position.Y) < SwipeMaximalHeight;
                     
                }
            }
        }

        private void LookForGesture()
        {

            //from http://kinecttoolkit.codeplex.com/
            if (ScanPositions((p1, p2) => Math.Abs(p2.Joint.Position.Y - p1.Joint.Position.Y) < SwipeMaximalHeight, // Height
                              (p1, p2) => p2.Joint.Position.X - p1.Joint.Position.X > -0.01f, // Progression to right
                              (p1, p2) => Math.Abs(p2.Joint.Position.X - p1.Joint.Position.X) > SwipeMinimalLength, // Length
                              SwipeMininalDuration, SwipeMaximalDuration)) // Duration
            {
                Console.WriteLine("Right Swipe Detected!");
            }
        }
    }
}