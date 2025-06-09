#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))]
public class SaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector elements (like 'someValue')
        DrawDefaultInspector();

        // Get a reference to the script instance we are inspecting
        SaveManager myScript = (SaveManager)target;

        // Add a button to the Inspector
        // GUILayout.Button returns true when the button is clicked
        if (GUILayout.Button("Delete Saved Game"))
        {
            // Call the public method on your script
            myScript.DeleteSaveGame();
        }
    }
}
#endif