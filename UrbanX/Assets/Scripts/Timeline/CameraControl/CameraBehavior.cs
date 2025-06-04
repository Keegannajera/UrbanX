using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class CameraBehavior : PlayableBehaviour
{
    [SerializeField] private bool m_Change_Pos;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private bool m_Change_Rotation;
    [SerializeField] private Quaternion m_Rotation;
    [SerializeField] private float m_Size;
    private Vector3 originalPos;
    private Quaternion originalRotation;
    private Vector3 ogPos = Vector3.negativeInfinity;
    private Transform controllingCam;

    



    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        controllingCam = playerData as Transform;
        if (controllingCam == null)
            return;
        
        float normalizedTime = (float)(playable.GetTime() / playable.GetDuration());

        if(m_Change_Pos)
            controllingCam.position = Vector3.Lerp(originalPos, targetPos, normalizedTime);
        if(m_Change_Rotation)
            controllingCam.localRotation = Quaternion.Lerp(originalRotation, m_Rotation, normalizedTime);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        controllingCam = info.output.GetUserData() as Transform;
        originalPos = controllingCam.position;
        originalRotation = controllingCam.localRotation;
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        if (controllingCam == null)
            return;

        //USeless
        //controllingCam.transform.rotation = originalRotation;
        //controllingCam.transform.position = originalPos;
        //ogPos = Vector3.negativeInfinity;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (controllingCam == null)
            return;
        //controllingCam.transform.rotation = originalRotation;
        //controllingCam.transform.position = originalPos;
        //ogPos = Vector3.negativeInfinity;
    }
}
