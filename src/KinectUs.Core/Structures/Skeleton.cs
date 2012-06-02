using System;
using Microsoft.Kinect;

namespace KinectUs.Core.Structures
{
    public class Skeleton
    {
        public FrameEdges ClippedEdges { get; set; }
        public Joint[] Joints { get; set; }
        public SkeletonPoint Position { get; set; }
        public int TrackingId { get; set; }
        public SkeletonTrackingState TrackingState { get; set; }

    }

    public class SkeletonData
    {
        public Skeleton[] Skeletons
        { get; set; }

        public SkeletonTrackingMode TrackingMode { get; set; }

        public int FrameNumber { get; set; }

        public Tuple<float, float, float, float> FloorClipPlane { get; set; }

        public long Timestamp { get; set; }
    }
}