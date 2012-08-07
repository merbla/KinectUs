using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using KinectUs.Core.Gestures;
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
            if (skeleton.Any())
            {
                Skeletons.OnNext(skeleton.First());
            }
        }
    }


    public class Pose : ICloneable
    {
        private readonly List<float> _data;
        private readonly bool[] _jointTrackOptions;
        private readonly Skeleton _skeletonData;

        public Pose(Skeleton data)
        {
            DurationMin = TimeSpan.FromMilliseconds(0.0);
            DurationMax = TimeSpan.FromMilliseconds(500.0);
            _jointTrackOptions = new bool[20];
            TrackXAxis = true;
            TrackYAxis = true;
            TrackZAxis = false;
            if (data.TrackingState != SkeletonTrackingState.Tracked)
                return;
            _skeletonData = data;
            var list1 = new List<float>();
            int num1 = 0;
            int num2 = checked(data.Joints.Count - 1);
            int num3 = num1;
            while (num3 <= num2)
            {
                List<float> list2 = list1;
                Joint joint = data.Joints[(JointType) num3];
                double num4 = joint.Position.X;
                list2.Add((float) num4);
                List<float> list3 = list1;
                joint = data.Joints[(JointType) num3];
                double num5 = joint.Position.Y;
                list3.Add((float) num5);
                List<float> list4 = list1;
                joint = data.Joints[(JointType) num3];
                double num6 = joint.Position.Z;
                list4.Add((float) num6);
                checked
                {
                    ++num3;
                }
            }
            _data = list1;
        }

        public Pose(List<float> data)
        {
            DurationMin = TimeSpan.FromMilliseconds(0.0);
            DurationMax = TimeSpan.FromMilliseconds(500.0);
            _jointTrackOptions = new bool[20];
            TrackXAxis = true;
            TrackYAxis = true;
            TrackZAxis = false;
            _data = data;
        }

        public Pose(byte[] data)
        {
            DurationMin = TimeSpan.FromMilliseconds(0.0);
            DurationMax = TimeSpan.FromMilliseconds(500.0);
            _jointTrackOptions = new bool[20];
            TrackXAxis = true;
            TrackYAxis = true;
            TrackZAxis = false;
            _data = new List<float>();
            int num1 = 0;
            int num2 = checked(data.Length - 1);
            int startIndex = num1;
            while (startIndex <= num2)
            {
                _data.Add(BitConverter.ToSingle(data, startIndex));
                checked
                {
                    startIndex += 4;
                }
            }
            int index = 0;
            do
            {
                _jointTrackOptions[index] = true;
                checked
                {
                    ++index;
                }
            } while (index <= 19);
        }

        public string Name { [DebuggerNonUserCode]
        get; [DebuggerNonUserCode]
        set; }

        public bool Matched { [DebuggerNonUserCode]
        get; [DebuggerNonUserCode]
        set; }

        public long MatchedTicks { [DebuggerNonUserCode]
        get; [DebuggerNonUserCode]
        set; }

        public float Delta { [DebuggerNonUserCode]
        get; [DebuggerNonUserCode]
        set; }

        public Skeleton SkeletonData
        {
            get { return _skeletonData; }
        }

        public TimeSpan DurationMin { get; set; }

        public TimeSpan DurationMax { get; set; }

        public List<float> RawData
        {
            get { return _data; }
        }

        public bool[] JointTrackOptions
        {
            get { return _jointTrackOptions; }
        }

        public bool TrackXAxis { [DebuggerNonUserCode]
        get; [DebuggerNonUserCode]
        set; }

        public bool TrackYAxis { [DebuggerNonUserCode]
        get; [DebuggerNonUserCode]
        set; }

        public bool TrackZAxis { [DebuggerNonUserCode]
        get; [DebuggerNonUserCode]
        set; }

        public bool TrackLeftAnkle
        {
            get { return _jointTrackOptions[14]; }
            set { _jointTrackOptions[14] = value; }
        }

        public bool TrackRightAnkle
        {
            get { return _jointTrackOptions[18]; }
            set { _jointTrackOptions[18] = value; }
        }

        public bool TrackLeftHand
        {
            get { return _jointTrackOptions[7]; }
            set { _jointTrackOptions[7] = value; }
        }

        public bool TrackRightHand
        {
            get { return _jointTrackOptions[11]; }
            set { _jointTrackOptions[11] = value; }
        }

        public bool TrackLeftElbow
        {
            get { return _jointTrackOptions[5]; }
            set { _jointTrackOptions[5] = value; }
        }

        public bool TrackRightElbow
        {
            get { return _jointTrackOptions[9]; }
            set { _jointTrackOptions[9] = value; }
        }

        public bool TrackLeftShoulder
        {
            get { return _jointTrackOptions[4]; }
            set { _jointTrackOptions[4] = value; }
        }

        public bool TrackRightShoulder
        {
            get { return _jointTrackOptions[8]; }
            set { _jointTrackOptions[8] = value; }
        }

        public bool TrackLeftKnee
        {
            get { return _jointTrackOptions[13]; }
            set { _jointTrackOptions[13] = value; }
        }

        public bool TrackRightKnee
        {
            get { return _jointTrackOptions[17]; }
            set { _jointTrackOptions[17] = value; }
        }

        public bool TrackLeftFoot
        {
            get { return _jointTrackOptions[15]; }
            set { _jointTrackOptions[15] = value; }
        }

        public bool TrackRightFoot
        {
            get { return _jointTrackOptions[19]; }
            set { _jointTrackOptions[19] = value; }
        }

        public bool TrackLeftHip
        {
            get { return _jointTrackOptions[12]; }
            set { _jointTrackOptions[12] = value; }
        }

        public bool TrackRightHip
        {
            get { return _jointTrackOptions[16]; }
            set { _jointTrackOptions[16] = value; }
        }

        public bool TrackHead
        {
            get { return _jointTrackOptions[3]; }
            set { _jointTrackOptions[3] = value; }
        }

        public bool TrackSpine
        {
            get { return _jointTrackOptions[1]; }
            set { _jointTrackOptions[1] = value; }
        }

        public bool TrackShoulderCenter
        {
            get { return _jointTrackOptions[2]; }
            set { _jointTrackOptions[2] = value; }
        }

        public bool TrackHipCenter
        {
            get { return _jointTrackOptions[0]; }
            set { _jointTrackOptions[0] = value; }
        }

        public bool TrackLeftWrist
        {
            get { return _jointTrackOptions[6]; }
            set { _jointTrackOptions[6] = value; }
        }

        public bool TrackRightWrist
        {
            get { return _jointTrackOptions[10]; }
            set { _jointTrackOptions[10] = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            var pose1 = new Pose(RawData);
            Pose pose2 = pose1;
            pose2.Delta = Delta;
            pose2.DurationMax = DurationMax;
            pose2.DurationMin = DurationMin;
            int num1 = 0;
            int num2 = checked(pose2.JointTrackOptions.Length - 1);
            int index = num1;
            while (index <= num2)
            {
                pose2.JointTrackOptions[index] = JointTrackOptions[index];
                checked
                {
                    ++index;
                }
            }
            pose2.Matched = Matched;
            pose2.MatchedTicks = MatchedTicks;
            pose2.Name = Name;
            pose2.TrackXAxis = TrackXAxis;
            pose2.TrackYAxis = TrackYAxis;
            pose2.TrackZAxis = TrackZAxis;
            return pose1;
        }

        #endregion

        public byte[] GetBytes()
        {
            var numArray = new byte[checked(_data.Count*4 - 1 + 1)];
            int num1 = 0;
            int num2 = checked(_data.Count - 1);
            int index = num1;
            while (index <= num2)
            {
                Array.Copy(BitConverter.GetBytes(_data[index]), 0, numArray, checked(index*4), 4);
                checked
                {
                    ++index;
                }
            }
            return numArray;
        }
    }
}