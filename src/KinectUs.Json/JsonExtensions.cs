using System.Collections.Generic;
using KinectUs.Core.Structures;
using Newtonsoft.Json;
using JsonSerializer = ServiceStack.Text.JsonSerializer;

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
            //var json = JsonSerializer.SerializeToString()<Microsoft.Kinect.Joint>(joint);

            var json = JsonConvert.SerializeObject(joint, Formatting.Indented);
            return json;
        }

        public static string ToJson(this Microsoft.Kinect.JointCollection joints)
        {
            var json = JsonConvert.SerializeObject(joints, Formatting.Indented);
            return json;
        }
    }
}