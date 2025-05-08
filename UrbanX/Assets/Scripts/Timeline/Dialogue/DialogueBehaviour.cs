using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    [SerializeField] private string dialogueText;
    private TMP_Text text;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        text = playerData as TMP_Text;
        
        if(text == null)
            return;
        
        text.text = dialogueText;
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        if(text == null)
            return;
        
        text.text = "";
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if(text == null)
            return;
        
        text.text = "";
    }
}
