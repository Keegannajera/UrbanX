using UnityEngine;
using UnityEngine.Timeline;

[TrackColor(1,0,1)]
[TrackClipType(typeof(LightClip))]
[TrackBindingType(typeof(Light))]
public class LightTrack : TrackAsset
{
}
