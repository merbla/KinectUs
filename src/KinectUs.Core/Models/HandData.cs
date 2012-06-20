using System;
using Microsoft.Kinect;

namespace KinectUs.Core.Models
{
    public class HandData
    {
        public DateTime Time { get; set; }
        public SkeletonPoint Position { get; set; }
        public Joint Joint { get; set; }
    }
}