using System;
using System.Collections.Generic;
using KinectUs.Core.Structures;
using Newtonsoft.Json;


namespace KinectUs.Json
{
    public static class JsonExtensions
    {
        //public static string ToJson(this SkeletonFrameData skeleton)
        //{
        //    var json = JsonSerializer.SerializeToString(skeleton);
        //    return json;
        //}

        //public static string ToJson(this Microsoft.Kinect.Skeleton skeleton)
        //{
        //    var json = JsonSerializer.SerializeToString(skeleton);
        //    return json;
        //}

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
    }

    public class KinectEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return (

                       objectType == typeof (Microsoft.Kinect.JointTrackingState) ||
                       objectType == typeof (Microsoft.Kinect.SkeletonTrackingState) ||
                       objectType == typeof (Microsoft.Kinect.JointType)

                   );
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Action)
            {
                writer.WriteValue(Enum.GetName(typeof(Action), (Action)value));// or something else
                return;
            }

            base.WriteJson(writer, value, serializer);
        }
    } 
}