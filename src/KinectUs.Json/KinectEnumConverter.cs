using System;
using Microsoft.Kinect;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KinectUs.Json
{
    public class KinectEnumConverter : StringEnumConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (
                       objectType == typeof (FrameEdges) ||
                       objectType == typeof (DepthRange) ||
                       objectType == typeof (EchoCancellationMode) ||
                       objectType == typeof (SkeletonTrackingMode) ||
                       objectType == typeof (JointTrackingState) ||
                       objectType == typeof (SkeletonTrackingState) ||
                       objectType == typeof (JointType)
                   );
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Action)
            {
                writer.WriteValue(Enum.GetName(typeof (Action), value)); // or something else
                return;
            }

            base.WriteJson(writer, value, serializer);
        }
    }
}