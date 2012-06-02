using System.Collections.Generic;
using KinectUs.Core.Structures;
using ServiceStack.Text;

namespace KinectUs.Json
{
    public static class JsonExtensions
    {
        public static string ToJson(this SkeletonFrameData skeleton)
        {
            var json = JsonSerializer.SerializeToString(skeleton);
            return json;
        }

        public static string ToJson(this Microsoft.Kinect.Skeleton skeleton)
        {
            var json = JsonSerializer.SerializeToString(skeleton);
            return json;
        }

        public  static string ToJson(this Microsoft.Kinect.Joint joint)
        {
            var json = JsonSerializer.SerializeToString<Microsoft.Kinect.Joint>(joint);
            return json;
        }

        public static string ToJson(this Microsoft.Kinect.JointCollection joints)
        {
            var jointsAsJson = new List<string>();
            foreach (Microsoft.Kinect.Joint joint in joints)
            {
                jointsAsJson.Add(joint.ToJson());
            }
            return JsonSerializer.SerializeToString(jointsAsJson);
        }
    }
}