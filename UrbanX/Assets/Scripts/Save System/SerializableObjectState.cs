using System;
using System.Collections.Generic;
using UnityEngine;

// Wraps the saved state data for a single object
[System.Serializable]
public class SerializableObjectState
{
    // Uses a SerializableDictionary to store the state JSON strings for each component
    // Key is the Type Name of ISaveableJson, Value is the JSON string of that component's state
    public SerializableDictionary<string, string> componentStatesJson = new SerializableDictionary<string, string>();

    // Can add other object-level states here, for example, the active state
    public bool isActive = true;
}