using UnityEngine;

public class SaveTest : MonoBehaviour, ISaveable
{
    private ISaveable _saveableImplementation;

    public object CaptureState()
    {
        var test = new ComponentState
        {
            position = new SerializableVector3(transform.position),
            rotation = new SerializableQuaternion(transform.rotation),
        };
        return test;
    }

    public void RestoreState(object state)
    {
        var loadedState = (ComponentState)state;
        if (loadedState == null) return;
        
        transform.position = loadedState.position.ToVector3();
        transform.rotation = loadedState.rotation.ToQuaternion();
    }
}
