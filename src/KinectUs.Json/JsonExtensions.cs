using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Kinect;
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


        public static string ToJson(this Microsoft.Kinect.Skeleton skeleton)
        {

            var json = JsonConvert.SerializeObject(skeleton,  new KinectEnumConverter());
            return json;
        }

        public static string ToJsonPretty(this Microsoft.Kinect.Skeleton skeleton)
        {
            
            var json = JsonConvert.SerializeObject(skeleton, Formatting.Indented, new KinectEnumConverter());
            return json;
        }

        public static IEnumerable<Microsoft.Kinect.Skeleton> ToSkeletons(this string json)
        {
            if (json == "[]")
            {
                return new List<Skeleton>();
            }
            var skeletons = JsonConvert.DeserializeObject<IEnumerable<Microsoft.Kinect.Skeleton>>(json, new KinectEnumConverter());
            return skeletons;
        }

        public static Microsoft.Kinect.Skeleton ToSkeleton(this string json)
        {
            if (json=="[]")
            {
                return null;
            }
            try
            {
                var skeleton = JsonConvert.DeserializeObject<Microsoft.Kinect.Skeleton>(json, new KinectEnumConverter());
                return skeleton;
            }
            catch (Exception exception)
            {
                return null;
            }
            

        }

        public static string ToJsonPretty(this IEnumerable<Microsoft.Kinect.Skeleton> skeletons)
        {
            var json = JsonConvert.SerializeObject(skeletons, Formatting.Indented, new KinectEnumConverter());
            return json;
        }


        public static string ToJson(this IEnumerable<Microsoft.Kinect.Skeleton> skeletons)
        {
            var json = JsonConvert.SerializeObject(skeletons, new KinectEnumConverter());
            return json;
        }
    }
}