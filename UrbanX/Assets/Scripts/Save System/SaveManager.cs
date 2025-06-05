using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement; 

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private bool newGameStart;

    private GameSaveData _currentGameSaveData; // The current game save data object
    private string _saveFilePath; // Full path to the save file

    // Save file name
    private const string SAVE_FILE_NAME = "gameSave.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Set the full path to save the data
            _saveFilePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            Debug.Log("Save file path: " + _saveFilePath);

            // Call InitializeGameData after setting the save file path
            InitializeGameData();

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else if (Instance != null && Instance != this)
        {
            // Ensure only one SaveManager exists
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when SaveManager is destroyed to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void StartNewGame(bool mode)
    {
        newGameStart = mode;
        Debug.Log(newGameStart);
    }
    
    public void InitializeGameData()
    {
        // Attempt to load the save game
        _currentGameSaveData = LoadGameDataFromFile();

        if (_currentGameSaveData == null)
        {
            Debug.Log("No save data found. Creating new game data.");
            // If loading failed or the file doesn't exist, create new game data
            _currentGameSaveData = new GameSaveData();

            // TODO: can set the initial global data for a new game here, If it's a new game, might need to load the first scene
        }
        else
        {
             Debug.Log("Game data initialized from loaded save.");
             // Here can apply the loaded global data to the corresponding systems in the game, like player data

             // TODO: Based on the save data, decide which scene to load and start the scene loading process
             // string sceneToLoad = _currentGameSaveData.lastPlayedScene;
             // SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Saves the current game state
    public void SaveGame()
    {
        // Before saving, first capture the state of all SaveableEntityJson objects in the current scene
        CaptureStateFromCurrentScene();

        // Use JsonUtility to serialize the entire GameSaveData object
        string json = JsonUtility.ToJson(_currentGameSaveData, true);

        try
        {
            // Write the JSON string to the file
            File.WriteAllText(_saveFilePath, json);
            Debug.Log($"Game saved successfully to {_saveFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save game to {_saveFilePath}: {e.Message}");
        }
    }

    // Attempts to load game data from the file
    private GameSaveData LoadGameDataFromFile()
    {
        if (File.Exists(_saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(_saveFilePath);
                // Use JsonUtility to deserialize the JSON string into a GameSaveData object
                GameSaveData loadedData = JsonUtility.FromJson<GameSaveData>(json);

                if (loadedData == null)
                {
                    Debug.LogError($"Failed to deserialize game data from {_saveFilePath}. File might be corrupted or empty.");
                    return null;
                }

                Debug.Log($"Game data loaded from {_saveFilePath}");
                return loadedData;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading or deserializing save file {_saveFilePath}: {e.Message}");
                // If loading fails, return null indicating no usable save data
                return null;
            }
        }
        else
        {
            Debug.Log("No save file found.");
            return null;
        }
    }

    // Called when a scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"SaveManager: Scene loaded: {scene.name} New game start: {newGameStart}");

        // After the scene is loaded, apply the saved state for the current scene
        if (_currentGameSaveData != null && !newGameStart)
        {
            ApplySavedStateToCurrentScene();
        }
        else
        {
             Debug.Log("SaveManager: No game data loaded, skipping scene state application.");
        }
    }


    // Captures the state of all SaveableEntityJson objects in the current active scene
    private void CaptureStateFromCurrentScene()
    {
        // Get the name of the current active scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Find or create the save data structure for the current scene within the current game data
        SceneSaveData currentSceneData = _currentGameSaveData.GetSceneData(currentSceneName);
        if (currentSceneData == null)
        {
            currentSceneData = new SceneSaveData(currentSceneName);
            // If it's new scene data, add it to the list of all scene data in the game data
            _currentGameSaveData.SetSceneData(currentSceneData);
        }
        else
        {
             // If it already exists, clear the old scene state data, preparing to store the new state
             currentSceneData.objectStates.Clear();
             // TODO: Might add some scene data here, like if the scene is completed, etc.
        }


        // Find all SaveableEntityJson objects in the current active scene
        // TODO: Would include inactive objects. If only want to save active objects, add a check.
        var saveableEntities = FindObjectsByType<SaveableEntityJson>(FindObjectsSortMode.None);

        foreach (var entity in saveableEntities)
        {
            SerializableObjectState serializableObjectState = entity.CaptureStateJson();

            // Store the object's unique ID and its serialized state data within the scene data
            currentSceneData.objectStates[entity.UniqueIdentifier] = serializableObjectState;
        }
        Debug.Log($"SaveManager: Captured state for scene: {currentSceneName} from {saveableEntities.Length} saveable entities.");
    }

    // Applies the saved state to the current active scene
    public void ApplySavedStateToCurrentScene()
    {
        // Ensure game data has been loaded
        if (_currentGameSaveData == null)
        {
             Debug.LogWarning("SaveManager: Cannot apply scene state, no game data loaded.");
             return;
        }

        // Get the name of the current active scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Get the save data for the current scene from the loaded game data
        SceneSaveData savedSceneData = _currentGameSaveData.GetSceneData(currentSceneName);

        if (savedSceneData == null)
        {
            Debug.Log($"SaveManager: No saved data found for current scene: {currentSceneName}. Skipping state application.");
            return;
        }

        // Use the static lookup table inside SaveableEntityJson to quickly find objects in the scene
        // After the scene loads and Awake runs, _activeSaveableEntities should contain all active objects
        // Make a copy of the dictionary so we can remove items during iteration
        var activeEntities = new Dictionary<string, SaveableEntityJson>(SaveableEntityJson._activeSaveableEntities);

        int appliedCount = 0;
        int notFoundCount = 0;

        // Iterate through each object state entry in the saved scene data
        foreach (var objStateEntry in savedSceneData.objectStates)
        {
            string uniqueId = objStateEntry.Key;
            SerializableObjectState savedObjectState = objStateEntry.Value; // This is the object containing component state JSON strings

            // Find the corresponding SaveableEntityJson object in the currently active scene objects
            if (activeEntities.TryGetValue(uniqueId, out SaveableEntityJson entity))
            {
                // Apply the saved state data to it
                entity.RestoreStateJson(savedObjectState);
                appliedCount++;

                // Remove from the list of active entities, useful for later handling of objects not present in the save
                activeEntities.Remove(uniqueId);
            }
            else
            {
                notFoundCount++;
                // The object does not exist in the current scene (it might have been destroyed after saving, or the current scene is new and needs object creation based on the save)
                // TODO: Handle objects that exist in the save but not in the current scene (instantiation logic)
                // Handling could be look up a Prefab by uniqueId and instantiate it
                Debug.LogWarning($"SaveManager: Object with ID {uniqueId} not found in current scene {currentSceneName}. Need to handle instantiation.");
            }
        }

        // TODO: Handle objects that exist in the current scene but not in the save (destruction logic)
        // For example, if an enemy was defeated when the game was saved, but the Prefab is spawned by default when the scene loads, you would need to destroy this Prefab instance.
        int destroyedCount = 0;
        
        foreach(var remainingEntity in activeEntities.Values)
        {
             Debug.Log($"SaveManager: Object {remainingEntity.gameObject.name} with ID {remainingEntity.UniqueIdentifier} found in scene but not in save data. Destroying.");
             DestroyImmediate(remainingEntity.gameObject);
             destroyedCount++;
        }
        


        Debug.Log($"SaveManager: Applied saved state to scene: {currentSceneName}. Applied {appliedCount}, Not Found {notFoundCount}.");
        if (destroyedCount > 0) Debug.Log($"SaveManager: Destroyed {destroyedCount} objects not present in save data for scene: {currentSceneName}.");
    }
    
    private void OnSceneUnloaded(Scene current)
    {
        // 在这里清空静态字典，为下一个场景做准备
        SaveableEntityJson._activeSaveableEntities.Clear();
        Debug.Log($"Scene {current.name} unloaded. Static saveable entities cleared.");
    }


    // Clears all save game files
    public void DeleteSaveGame()
    {
        _saveFilePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        if (File.Exists(_saveFilePath))
        {
            try
            {
                File.Delete(_saveFilePath);
                // Reset the current game data to new empty data
                _currentGameSaveData = new GameSaveData();
                Debug.Log($"Save game deleted from {_saveFilePath}!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to delete save game from {_saveFilePath}: {e.Message}");
            }
        }
        else
        {
            Debug.Log("SaveManager: No save game found to delete.");
        }
    }

    // Public interface: Get the current global data. Other systems can access or modify global data through this.
    public GlobalData GetGlobalData()
    {
        if (_currentGameSaveData == null)
        {
             Debug.LogError("SaveManager: Cannot get global data, game data is not initialized.");
             // Return a new default data or null, depending on your error handling strategy
             return new GlobalData();
        }
        return _currentGameSaveData.globalData;
    }

    // Public interface: Set the global data 
    // Note: Modifying the object returned by GetGlobalData() is also possible, but this method provides a clear setting point
    public void SetGlobalData(GlobalData globalData)
    {
         if (_currentGameSaveData == null)
         {
              Debug.LogError("SaveManager: Cannot set global data, game data is not initialized.");
              return;
         }
         _currentGameSaveData.globalData = globalData;
    }

    // Public interface: Check if a save game file exists
    public bool HasSaveGame()
    {
        return File.Exists(_saveFilePath);
    }
}