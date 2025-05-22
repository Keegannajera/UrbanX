using System;
using System.Collections.Generic;
using UnityEngine;

// Defines the save data structure for the entire game
[System.Serializable]
public class GameSaveData
{
    // Global game data
    public GlobalData globalData = new GlobalData();

    // List of data for all scenes
    public List<SceneSaveData> sceneDataList = new List<SceneSaveData>();
    

    public GameSaveData()
    {
        // Default constructor
    }

    // Finds save data for a specific scene by name
    public SceneSaveData GetSceneData(string sceneName)
    {
        return sceneDataList.Find(data => data.sceneName == sceneName);
    }

    // Adds or updates save data for a specific scene
    public void SetSceneData(SceneSaveData data)
    {
        var existingData = GetSceneData(data.sceneName);
        if (existingData != null)
        {
            sceneDataList.Remove(existingData);
        }
        sceneDataList.Add(data);
    }
}