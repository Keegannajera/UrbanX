using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CameraClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] CameraBehavior template = new CameraBehavior();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }


    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<CameraBehavior>.Create(graph, template);
        //return _playableAssetImplementation.CreatePlayable(graph, owner);
    }
}
