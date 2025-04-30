using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private DialogueBehaviour template = new DialogueBehaviour();
    
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<DialogueBehaviour>.Create(graph, template);
    }
}