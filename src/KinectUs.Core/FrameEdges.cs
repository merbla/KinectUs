using System;

namespace KinectUs.Core
{
    [Flags]
    public enum FrameEdges
    {
        Bottom = 8,
        Left = 2,
        None = 0,
        Right = 1,
        Top = 4
    }
}