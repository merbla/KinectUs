using ZMQ;

namespace KinectUs.Publisher
{
    public class PublisherConfiguration : IPublisherConfiguration
    {
        #region IPublisherConfiguration Members

        public uint ProtoBufPublishPort { get; set; }
        public uint JsonPublishPort { get; set; }
        public int NumberOfThreads { get; set; }
        public uint PullPort { get; set; }
        public Transport Transport { get; set; }

        #endregion
    }

    public interface IPublisherConfiguration
    {
        uint ProtoBufPublishPort { get; set; }
        uint JsonPublishPort { get; set; }
        int NumberOfThreads { get; set; }
        uint PullPort { get; set; }
        Transport Transport { get; set; }
    }
}