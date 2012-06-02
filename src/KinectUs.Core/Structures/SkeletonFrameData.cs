using System;

namespace KinectUs.Core.Structures
{
    public class SkeletonFrameData
    {
        public Tuple<float, float, float, float> FloorClipPlane { get; set; }
        public int FrameNumber { get; set; }
        public int SkeletonArrayLength { get; set; }
        public long Timestamp { get; set; }
        public Skeleton[] Skeletons { get; set; }
    }
}