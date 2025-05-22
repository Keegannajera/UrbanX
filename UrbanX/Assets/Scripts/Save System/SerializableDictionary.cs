using UnityEngine;
using System;
using System.Collections.Generic;

// Custom Serializable Dictionary for JsonUtility
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    // Before serialization, copy dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // After deserialization, copy lists to dictionary
    public void OnAfterDeserialize()
    {
        this.Clear();
        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary with unequal key and value counts: " + keys.Count + " vs " + values.Count);
            return;
        }
        for (int i = 0; i < keys.Count; i++)
        {
            // Avoid adding duplicate keys if the list somehow contains them after deserialization
            if (!this.ContainsKey(keys[i]))
            {
                this.Add(keys[i], values[i]);
            }
            else
            {
                // Handle potential duplicate keys during deserialization
                Debug.LogWarning($"Duplicate key found during dictionary deserialization: {keys[i]}. Overwriting.");
                this[keys[i]] = values[i]; // Overwrite existing entry
            }
        }
    }
}