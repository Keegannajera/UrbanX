// ISaveableJson.cs
using System;

// Defines the interface that components needing their state saved must implement
// Convention: Implementers know how to serialize their own state into a JSON string and deserialize from a JSON string to restore state
public interface ISaveable
{
    // Returns the state data object for the current component
    // Note: This object itself MUST be serializable (e.g., marked with [System.Serializable])
    object CaptureState();

    // Receives the JSON string of the component's state and applies it to the component after deserialization
    void RestoreStateFromJson(string jsonState);

    // Returns the Type of the data object returned by the CaptureState method, to help SaveManager with deserialization
    // Note: The Type returned by GetStateType() MUST match the actual type of the object returned by CaptureState()
    Type GetStateType();
}