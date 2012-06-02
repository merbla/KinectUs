using ZMQ;
namespace KinectUs.Publisher
{
    public interface IPublisherConfiguration
    {
        uint ProtoBufPublishPort { get; set; }
        uint JsonPublishPort { get; set; }
        int NumberOfThreads { get; set; }
        uint PullPort { get; set; }
         ZMQ.Transport Transport { get; set; }
    }
}