using System.Collections.Generic;
using Newtonsoft.Json;


namespace KinectUs.Json
{
    public static class JsonExtensions
    {
        public  static string ToJson(this Microsoft.Kinect.Joint joint)
        {
            var json = JsonConvert.SerializeObject(joint, Formatting.Indented, new KinectEnumConverter());
            return json;
        }

        public static string ToJson(this Microsoft.Kinect.JointCollection joints)
        {
            var json = JsonConvert.SerializeObject(joints, Formatting.Indented, new KinectEnumConverter());
            return json;
        }

        public static string ToJsonPretty(this Microsoft.Kinect.Skeleton skeleton)
        {
            
            var json = JsonConvert.SerializeObject(skeleton, Formatting.Indented, new KinectEnumConverter());
            return json;
        }


        public static string ToJsonPretty(this IEnumerable<Microsoft.Kinect.Skeleton> skeletons)
        {
            var json = JsonConvert.SerializeObject(skeletons, Formatting.Indented, new KinectEnumConverter());
            return json;
        }
    }
}