using System.Collections.Generic;
using System.IO;
using Microsoft.Kinect;
using ProtoBuf.Meta;

namespace KinectUs.ProtoBuf
{
    public static class ProtoBufExtensions
    {
        private static RuntimeTypeModel _model;


        private static readonly string[] _jointProperties = { 
                                                                   "Position",   
                                                                   "TrackingState",
                                                                   "JointType"
                                                               };
        private static readonly string[] _skeletonProperties = {
                                                                   "Joints",
                                                                   "Position", 
                                                                   "TrackingId", 
                                                                   "TrackingState",
                                                                   "ClippedEdges",
                                                                   "BoneOrientations"
                                                               };

        public static RuntimeTypeModel Model
        {
            get
            {
                if (_model == null)
                {
                    _model = TypeModel.Create();
                    _model.Add(typeof (Skeleton), false)
                        .Add(_skeletonProperties);

                    _model.Add(typeof (Joint), false)
                        .Add(_jointProperties);

                }

                return _model;
            }
        }

        public static Stream ToStream(this IEnumerable<Skeleton> skeletons, Stream destination)
        {
            Model.Serialize(destination, skeletons);
            return destination;
        }
    }
}