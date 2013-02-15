using Microsoft.Kinect;

namespace KinectUs.Core
{
    public static class Extensions
    {
        public static Joint LeftHand(this Skeleton skeleton)
        {
            return skeleton.Joints[JointType.HandLeft];
        }

        public static Joint RightHand(this Skeleton skeleton)
        {
            return skeleton.Joints[JointType.HandRight];
        }
    }
}