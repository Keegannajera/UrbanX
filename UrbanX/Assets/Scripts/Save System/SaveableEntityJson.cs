using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Allow running update in editor for generating UID
[ExecuteAlways]
public class SaveableEntityJson : MonoBehaviour
{
    //DO NOT CHANGE MANUALLY
    [SerializeField] private string uniqueIdentifier = "";

    public string UniqueIdentifier => uniqueIdentifier;

    // Dictionary to use UID to find entity
    public static Dictionary<string, SaveableEntityJson> _activeSaveableEntities = new Dictionary<string, SaveableEntityJson>();

    private void Awake()
    {
        // Validate the UID
        if (Application.isPlaying && (string.IsNullOrEmpty(uniqueIdentifier)))// || !IsValidGuid(uniqueIdentifier)))
        {
             Debug.LogError($"SaveableEntity on {gameObject.name} has an invalid or empty UniqueIdentifier in Play mode. Cannot register.");
             enabled = false;
             return;
        }

        // Prevent having the same object
        if (Application.isPlaying && _activeSaveableEntities.ContainsKey(uniqueIdentifier))
        {
             Debug.LogError($"Duplicate SaveableEntity ID found: {uniqueIdentifier} on {gameObject.name}. Destroying duplicate.");
             Destroy(gameObject); 
             return;
        }
        
        // Add self to the dictionary
        if (Application.isPlaying)
        {
            _activeSaveableEntities[uniqueIdentifier] = this;
        }
    }

    private void OnDestroy()
    {
        // Remove self from the save list
        if (Application.isPlaying && _activeSaveableEntities.ContainsKey(uniqueIdentifier))
        {
            _activeSaveableEntities.Remove(uniqueIdentifier);
        }
    }

    // ---- EDITOR ONLY ----
#if UNITY_EDITOR
    // Generating Unique ID
    private void Update()
    {
        if (Application.IsPlaying(gameObject)) return;

        if (string.IsNullOrEmpty(uniqueIdentifier) || !IsValidGuid(uniqueIdentifier))
        {
            uniqueIdentifier = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this); // Set object to edited
        }
    }

    private bool IsValidGuid(string id)
    {
        if (string.IsNullOrEmpty(id)) return false;
        try
        {
            System.Guid guid = new System.Guid(id);
            return guid != System.Guid.Empty;
        }
        catch (FormatException)
        {
            return false;
        }
    }
#endif
    // ---- EDITOR STUFF FINISHED----


    // Get all the ISabeable Data from this object to create Json file
    public SerializableObjectState CaptureStateJson()
    {
        var serializableObjectState = new SerializableObjectState();
        serializableObjectState.isActive = gameObject.activeSelf; 

        // Get all components with ISaveable
        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString(); 
            try
            {
                 object componentState = saveable.CaptureState();
                 // Turn the state to Json 
                 string componentStateJson = JsonUtility.ToJson(componentState);
                 serializableObjectState.componentStatesJson[typeName] = componentStateJson;
            }
            catch (Exception e)
            {
                 Debug.LogError($"Failed to capture state for component {typeName} on object {gameObject.name}: {e.Message}");
            }
        }
        return serializableObjectState;
    }

    // Use the Json file to apply data to each ISaveable
    public void RestoreStateJson(SerializableObjectState savedObjectState)
    {
        if (savedObjectState == null) return;

        gameObject.SetActive(savedObjectState.isActive);


        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();
            if (savedObjectState.componentStatesJson.TryGetValue(typeName, out string componentStateJson))
            {
                try
                {
                    saveable.RestoreStateFromJson(componentStateJson);
                }
                catch (Exception e)
                {
                     Debug.LogError($"Failed to restore state for component {typeName} on object {gameObject.name}: {e.Message}");
                }
            }
            else { Debug.LogWarning($"No saved state found for component {typeName} on object {gameObject.name}."); }
        }
    }

    // Use to find SaveableEntityJson by UID
    public static SaveableEntityJson Find(string uniqueIdentifier)
    {
        if (_activeSaveableEntities.TryGetValue(uniqueIdentifier, out SaveableEntityJson entity))
        {
            return entity;
        }
        return null;
    }

    
    public static void ClearActiveEntities()
    {
        _activeSaveableEntities.Clear();
    }
}