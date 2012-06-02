KinectUs - spreading the #awesome of the Kinect via ZeroMQ
=================================================================

Authors
-------

* [Matthew Erbs](http://merbla.com) on [Twitter](http://twitter.com/#!/matthewerbs)


Kinect Us
-------------------------------
The Kinect is an awesome piece of kit, however it has mainly been reserved for the Microsoft domain.  Via ZeroMQ, this project attempts
to open its connectivity to other platforms.


Requirements/Dependencies
-------------------------
You will need the Kinect SDK (v1.5) and the Speech SDK (v11) from [Kinect for Windows](http://www.microsoft.com/en-us/kinectforwindows/develop/developer-downloads.aspx) 


Planned Features
----------------

* Simple JSON string messages
* ProtoBuf or MsgPack messages for performance
* JSON structures for Colour
* JSON structures for Depth
* JSON structures for Audio
* JSON structures for Skeleton

Future Features
---------------

* Suggestions??

Example of JSON data
--------------------

```json
[
  {
    "TrackingState": "Tracked",
    "TrackingId": 8,
    "Position": {
      "X": 0.145386174,
      "Y": -0.06435618,
      "Z": 1.68396413
    },
    "Joints": [
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "HipCenter",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "Spine",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.231304377,
          "Y": 0.07501991,
          "Z": 1.782474
        },
        "JointType": "ShoulderCenter",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": 0.223919913,
          "Y": 0.25025335,
          "Z": 1.79188693
        },
        "JointType": "Head",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": 0.09578784,
          "Y": -0.000471509644,
          "Z": 1.85843074
        },
        "JointType": "ShoulderLeft",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": 0.008514807,
          "Y": -0.141752541,
          "Z": 1.76583743
        },
        "JointType": "ElbowLeft",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": -0.0305912644,
          "Y": 0.0435683578,
          "Z": 1.68105352
        },
        "JointType": "WristLeft",
        "TrackingState": "Inferred"
      },
      {
        "Position": {
          "X": -0.0184278246,
          "Y": 0.08992092,
          "Z": 1.66266572
        },
        "JointType": "HandLeft",
        "TrackingState": "Inferred"
      },
      {
        "Position": {
          "X": 0.376942754,
          "Y": 0.00340297259,
          "Z": 1.808434
        },
        "JointType": "ShoulderRight",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": 0.4047285,
          "Y": -0.153887331,
          "Z": 1.746834
        },
        "JointType": "ElbowRight",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": 0.3322568,
          "Y": -0.298559248,
          "Z": 1.66354811
        },
        "JointType": "WristRight",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": 0.3187368,
          "Y": -0.326050162,
          "Z": 1.62236464
        },
        "JointType": "HandRight",
        "TrackingState": "Tracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "HipLeft",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "KneeLeft",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "AnkleLeft",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "FootLeft",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "HipRight",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "KneeRight",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "AnkleRight",
        "TrackingState": "NotTracked"
      },
      {
        "Position": {
          "X": 0.0,
          "Y": 0.0,
          "Z": 0.0
        },
        "JointType": "FootRight",
        "TrackingState": "NotTracked"
      }
    ],
   
    "ClippedEdges": "None"
  }
]
```

License
-------

KinectUs is Open Source software released under the Apache 2.0 License.
Please see the `LICENSE` file for full license details.

