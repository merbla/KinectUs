using KinectUs.Json;
using Microsoft.Kinect;
using NUnit.Framework;

namespace KinectUs.Tests.Json
{
    [TestFixture]
    public class KinectEnumConvertFixture
    {
        [Test]
        public void JointTrackingStateIsSerialised()
        {
            var j = new Joint {TrackingState = JointTrackingState.Tracked};

            var json = j.ToJson();
            Assert.IsTrue(json.Contains("Tracked"));
        }
    }
}