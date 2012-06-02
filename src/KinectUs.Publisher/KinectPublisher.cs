﻿using System;
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
    public interface IKinectPublisher:IDisposable
    {
        void Start();
    }

    public class KinectPublisher : IKinectPublisher
    {
        private readonly IKinectManager _manager;
        private readonly IPublisherConfiguration _configuration;
        private Context _context;
        private Socket _publisher;
        private Socket _synchroniser;

        public KinectPublisher(IKinectManager manager, IPublisherConfiguration configuration)
        {
            _manager = manager;
            _configuration = configuration;
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
            _context = new Context(_configuration.NumberOfThreads);
            _publisher = _context.Socket(SocketType.PUB);
            _synchroniser = _context.Socket(SocketType.PULL);
            
            _publisher.Bind(_configuration.Transport, "*", _configuration.JsonPublishPort);
            
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
            _publisher.Send(skeletons.ToJsonPretty(), Encoding.Unicode);
        }
    }
}