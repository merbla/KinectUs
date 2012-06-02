using ZMQ;

namespace KinectUs.Publisher
{
    public class PublisherConfiguration : IPublisherConfiguration
    {
        public uint ProtoBufPublishPort { get; set; }
        public uint JsonPublishPort { get; set; }
        public int NumberOfThreads { get; set; }
        public uint PullPort { get; set; }
        public Transport Transport { get; set; }
    }
}