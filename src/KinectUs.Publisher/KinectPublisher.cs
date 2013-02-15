using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using KinectUs.Json;
using Microsoft.Kinect;
using ZMQ;
using Exception = System.Exception;
using Skeleton = Microsoft.Kinect.Skeleton;
using SkeletonTrackingState = Microsoft.Kinect.SkeletonTrackingState;


namespace KinectUs.Publisher
{
    public interface IKinectPublisher : IDisposable
    {
        void Start();
    }

    public class KinectPublisher : IKinectPublisher
    {
        private readonly IKinectManager _manager;
        private readonly IPublisherConfiguration _configuration;
        private Context _context;
        private Socket _jsonPublisher;
        private Socket _synchroniser;
        private Socket _protoBufPublisher;

        public KinectPublisher(IKinectManager manager, IPublisherConfiguration configuration)
        {
            _manager = manager;
            _configuration = configuration;
        }

        public void Dispose()
        {
            _manager.Dispose();
            _jsonPublisher.Dispose();
            _synchroniser.Dispose();
            _context.Dispose();
        }

        public void Start()
        {
            _context = new Context(_configuration.NumberOfThreads);
            _jsonPublisher = _context.Socket(SocketType.PUB);
            _protoBufPublisher = _context.Socket(SocketType.PUB);
            _synchroniser = _context.Socket(SocketType.PULL);
            
            _jsonPublisher.Bind(_configuration.Transport, "*", _configuration.JsonPublishPort);
            _protoBufPublisher.Bind(_configuration.Transport, "*", _configuration.ProtoBufPublishPort);
            
            //_synchroniser.Bind(Transport.TCP, "*", (uint)_zeroMqPullPort);
            //_synchroniser.Recv(); 

            if(_manager.Start() == KinectStatus.Connected)
            {
                _manager.Skeletons
                  .Select(ss => ss.Where(s => s.TrackingState != SkeletonTrackingState.NotTracked))
                  .Subscribe(OnNextSkeletons, OnNextSkeletonError);  
            }
            else
            {
                Console.WriteLine("Can't connect to Kinect!");
            }
            
        }

        private void OnNextSkeletonError(Exception exception)
        {
            //Exit?
            Console.WriteLine(exception.Message);
        }

        private void OnNextSkeletons(IEnumerable<Skeleton> skeletons)
        {
            var message = skeletons.ToJson();
            _jsonPublisher.Send(message, Encoding.Unicode);
            
#if DEBUG
           // Console.WriteLine("Published Skeleton");
#endif
            
        }
    }
}
