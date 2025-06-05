using UnityEngine;
using System;

// Put on to the object with savable entity
public class NormalSaveableComponentJson : MonoBehaviour, ISaveable
{
    public Vector3 initialPosition; 

    void Awake()
    {
        initialPosition = transform.position;
    }

    [System.Serializable]
    private class StateData // can be nested class like this, or other public class
    {
        public bool isCompleted;
        public SerializableVector3 position; 
        public SerializableQuaternion rotation; 
    }

    // Capture State to return the data
    public object CaptureState()
    {
        // 捕获状态并返回 StateData 对象
        return new StateData
        {
            position = new SerializableVector3(transform.position),
            rotation = new SerializableQuaternion(transform.rotation),
        };
    }

    // Read from the jsonState string to load data
    public void RestoreStateFromJson(string jsonState)
    {
        StateData loadedState = JsonUtility.FromJson<StateData>(jsonState);
        if (loadedState != null)
        {
            Debug.Log($"Loading pos form {this.gameObject.name}, with pos {loadedState.position.ToVector3()}");
            transform.position = loadedState.position.ToVector3();
            Debug.Log($"Loading pos form {this.gameObject.name}, with pos {this.gameObject.transform.position}");
            transform.rotation = loadedState.rotation.ToQuaternion();
        }
        else
        {
            Debug.LogError("State not found");
        }
    }
    
    //return state data of this obj
    public Type GetStateType()
    {
        return typeof(StateData);
    }

    // Reset the data 
    // TODO: not fully implement, need another filed storing initial data if using
    public void ResetState()
    {
        //transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);
        Debug.Log("Object state reset.");
    }
}