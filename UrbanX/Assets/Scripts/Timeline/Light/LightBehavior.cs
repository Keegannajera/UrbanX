using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class LightBehavior : PlayableBehaviour
{
    public Color _color;
    private Light _light;
    //Original color: FEFFCA
    
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        _light = playerData as Light;
        
        if(_light == null)
            return;
        
        _light.color = _color;
    }
}
