using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LightClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private LightBehavior template = new LightBehavior();
    
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<LightBehavior>.Create(graph, template);
    }
}
