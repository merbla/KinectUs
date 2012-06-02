namespace KinectUs.Core
{
    public class Skeleton
    {
        public FrameEdges ClippedEdges { get; set; }
        public Joint[] Joints { get; set; }
        public SkeletonPoint Position { get; set; }
        public int TrackingId { get; set; }
        public SkeletonTrackingState TrackingState { get; set; }


    }
}