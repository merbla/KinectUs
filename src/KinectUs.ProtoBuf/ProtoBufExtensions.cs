using System.Collections.Generic;
using System.IO;
using ProtoBuf.Meta;

namespace KinectUs.ProtoBuf
{
    public static class ProtoBufExtensions
    {
        private static RuntimeTypeModel _model;

        public static Stream ToStream(this IEnumerable<Microsoft.Kinect.Skeleton> skeletons, Stream destination)
        { 
            Model.Serialize(destination, skeletons);
            return destination;
        }

        public static RuntimeTypeModel Model
        {
            get
            {
                if(_model==null)
                {
                    _model = TypeModel.Create();
                    _model.Add(typeof(Microsoft.Kinect.Skeleton), false)
                                 .Add("Joints", "Position", "TrackingId", "TrackingState", "ClippedEdges", "BoneOrientations");
     
                }

                return _model;
            }
        
        }
    }
}