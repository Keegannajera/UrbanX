using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor; 

[ExecuteAlways] // Execute in editor for generating ID
public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string uniqueIdentifier = "";

    public string UniqueIdentifier => uniqueIdentifier;
    private static Dictionary<string, SaveableEntity> _activeSaveableEntities = new Dictionary<string, SaveableEntity>();

    private void Awake()
    {
        if (_activeSaveableEntities.ContainsKey(uniqueIdentifier))
        {
            Debug.LogError($"Duplicate SaveableEntity ID found: {uniqueIdentifier} on {gameObject.name}. This object will not be registered for saving/loading.");
            return;
        }
        if (string.IsNullOrEmpty(uniqueIdentifier))
        {
            uniqueIdentifier = System.Guid.NewGuid().ToString();
            Debug.LogWarning($"New object that is not on saving list {gameObject.name}. Generating ID {uniqueIdentifier}");
        }
        _activeSaveableEntities[uniqueIdentifier] = this;
    }

    private void OnDestroy()
    {
        if (_activeSaveableEntities.ContainsKey(uniqueIdentifier))
        {
            _activeSaveableEntities.Remove(uniqueIdentifier);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.IsPlaying(gameObject)) return;

        if (string.IsNullOrEmpty(uniqueIdentifier) || !IsValidGuid(uniqueIdentifier))
        {
            uniqueIdentifier = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this); 
        }
    }

    private bool IsValidGuid(string id)
    {
        if (string.IsNullOrEmpty(id)) return false;
        try
        {
            System.Guid guid = new System.Guid(id);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
#endif
    
    public object CaptureState()
    {
        var state = new Dictionary<string, object>();
        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }
        return state;
    }

    public void RestoreState(object state)
    {
        var stateDictionary = state as Dictionary<string, object>;
        if (stateDictionary == null) return;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();
            if (stateDictionary.TryGetValue(typeName, out object componentState))
            {
                saveable.RestoreState(componentState);
            }
        }
    }

    public static SaveableEntity Find(string uniqueIdentifier)
    {
        if (_activeSaveableEntities.TryGetValue(uniqueIdentifier, out SaveableEntity entity))
        {
            return entity;
        }
        return null;
    }
}