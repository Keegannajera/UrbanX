using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CameraClip : PlayableAsset, ITimelineClipAsset
{
    private ITimelineClipAsset _timelineClipAssetImplementation;
    private PlayableAsset _playableAssetImplementation;
    public ClipCaps clipCaps => _timelineClipAssetImplementation.clipCaps;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return _playableAssetImplementation.CreatePlayable(graph, owner);
    }
}
