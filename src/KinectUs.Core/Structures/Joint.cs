namespace KinectUs.Core.Structures
{
    public struct Joint
    {
        public JointType JointType { get; set; }
        public SkeletonPointData Position { get; set; }
        public JointTrackingState TrackingState { get; set; }
    }
}