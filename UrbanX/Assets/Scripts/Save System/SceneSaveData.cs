using System;
using System.Collections.Generic;
using UnityEngine;

// Defines the save data structure for a single scene
[System.Serializable]
public class SceneSaveData
{
    public string sceneName;
    // Uses a SerializableDictionary to store the state of each saveable object in this scene
    // Key is the UID, Value is the SerializableObjectState
    public SerializableDictionary<string, SerializableObjectState> objectStates = new SerializableDictionary<string, SerializableObjectState>();

    // Can add things like whether a certain event has been triggered in the scene
    public bool sceneInitialized = false; 

    public SceneSaveData(string name)
    {
        sceneName = name;
    }
}