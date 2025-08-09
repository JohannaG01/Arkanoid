#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class LevelDuplicatorMenu
{
    [MenuItem("Tools/Duplicate Selected Level")]
    public static void DuplicateSelectedLevel()
    {
        Level selectedLevel = Selection.activeObject as Level;
        
        if (selectedLevel == null)
        {
            Debug.LogWarning("Select a Level asset to duplicate.");
            return;
        }

        LevelDuplicator.DuplicateLevel(selectedLevel);
    }
}
#endif
