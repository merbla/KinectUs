﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using Microsoft.Kinect;
using ZMQ;
using Exception = System.Exception;

namespace KinectUs.Publisher
{
    public interface IKinectPublisher:IDisposable
    {
        void Start();
    }

    public class KinectPublisher : IKinectPublisher
    {
        private readonly IKinectManager _manager;
        private readonly int _zeroMqPublishPort;
        private readonly string _zeroMqTransport;
        private readonly int _zeroMqThreads;
        private readonly int _zeroMqPullPort;
        private Context _context;
        private Socket _publisher;
        private Socket _synchroniser;

        public KinectPublisher(IKinectManager manager, int zeroMqPublishPort, string zeroMqTransport, int zeroMqThreads, int zeroMqPullPort)
        {
            _manager = manager;
            _zeroMqPublishPort = zeroMqPublishPort;
            _zeroMqTransport = zeroMqTransport;
            _zeroMqThreads = zeroMqThreads;
            _zeroMqPullPort = zeroMqPullPort;
        }

        public void Dispose()
        {
            _manager.Dispose();
            _publisher.Dispose();
            _synchroniser.Dispose();
            _context.Dispose();
        }

        public void Start()
        {
            _context = new Context(_zeroMqThreads);
            _publisher = _context.Socket(SocketType.PUB);
            _synchroniser = _context.Socket(SocketType.PULL);
            
            _publisher.Bind(Transport.TCP, "*", (uint)_zeroMqPublishPort);
            _synchroniser.Bind(Transport.TCP, "*", (uint)_zeroMqPullPort);

            _synchroniser.Recv(); 

            _manager.Start();
            _manager.Skeletons
                .Select(ss => ss.Where(s => s.TrackingState != SkeletonTrackingState.NotTracked))
                .Subscribe(OnNextSkeletons, OnNextSkeletonError);
        }

        private void OnNextSkeletonError(Exception exception)
        {
            //Exit?
            Console.WriteLine(exception.Message);
        }

        private void OnNextSkeletons(IEnumerable<Skeleton> skeletons)
        {
            //publish on ZeroMq
            _publisher.Send(skeletons.Count().ToString() , Encoding.Unicode);
        }
    } 
}