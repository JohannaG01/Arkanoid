using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Save Level"))
        {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            Debug.Log("Level saved!");
        }
    }
}
