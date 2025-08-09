#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class LevelDuplicator
{
    public static void DuplicateLevel(Level original)
    {
        string path = AssetDatabase.GetAssetPath(original);
        string newPath = AssetDatabase.GenerateUniqueAssetPath(path);
        AssetDatabase.CopyAsset(path, newPath);
        AssetDatabase.Refresh();
        Debug.Log($"Duplicated level at {newPath}");
    }
}
#endif
