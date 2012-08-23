using System.Linq;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Microsoft.Kinect;

namespace KinectUs.Configuration.Utility
{
    public class ShellViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private const float RenderWidth = 640.0f;
        private const float RenderHeight = 480.0f;
        private const double JointThickness = 3;
        private const double BodyCenterThickness = 10;
        private const double ClipBoundsThickness = 10;
        private readonly Pen _inferredBonePen = new Pen(Brushes.Gray, 1);
        private readonly Brush _inferredJointBrush = Brushes.Yellow;
        private readonly Pen _trackedBonePen = new Pen(Brushes.Green, 6);
        private readonly Brush _trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private KinectSensor _activeSensor;
        private KinectSensorCollection _sensors;

        //public ShellViewModel(IWindowManager windowManager)
        //{
        //    _windowManager = windowManager;
        //}

        protected override void OnActivate()
        {
            base.OnActivate();
            _sensors = KinectSensor.KinectSensors;

            if(_sensors.Any(x => x.Status != KinectStatus.Connected))
            {
                Application.Current.Shutdown();
                
            }


            _activeSensor = _sensors.First(x => x.Status == KinectStatus.Connected);

            _activeSensor.SkeletonStream.Enable();
            _activeSensor.SkeletonFrameReady += (sender, args) => { };
            _activeSensor.Start();

            //TODO: Seated??
        }
        
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);
            DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);
            DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);


            DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);


            DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);


            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = _trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = _inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, SkeletonPointToScreen(joint.Position), JointThickness,
                                               JointThickness);
                }
            }
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            DepthImagePoint depthPoint = _activeSensor.MapSkeletonPointToDepth(skelpoint,
                                                                               DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
        
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0,
                              JointType jointType1)
        {
            var joint0 = skeleton.Joints[jointType0];
            var joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = _inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = _trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, SkeletonPointToScreen(joint0.Position),
                                    SkeletonPointToScreen(joint1.Position));
        }
    }
}