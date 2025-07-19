using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class UnusedAssetFinder : EditorWindow
{
    private string assetFolderPath = "Assets/Versatile Studio Assets/Demo City By Versatile Studio";
    private List<string> unusedAssets = new List<string>();
    private Vector2 scroll;

    [MenuItem("Tools/Find Unused Assets")]
    public static void ShowWindow()
    {
        GetWindow<UnusedAssetFinder>("Unused Asset Finder");
    }

    private void OnGUI()
    {
        GUILayout.Label("Find Unused Assets", EditorStyles.boldLabel);
        assetFolderPath = EditorGUILayout.TextField("Folder Path", assetFolderPath);

        if (GUILayout.Button("Scan for Unused Assets"))
        {
            FindUnusedAssets();
        }

        GUILayout.Space(10);
        scroll = GUILayout.BeginScrollView(scroll);
        foreach (var asset in unusedAssets)
        {
            GUILayout.Label(asset);
        }
        GUILayout.EndScrollView();
    }

    private void FindUnusedAssets()
    {
        unusedAssets.Clear();
        string[] allAssets = AssetDatabase.FindAssets("", new[] { assetFolderPath });

        foreach (string guid in allAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            if (Directory.Exists(path)) continue;

            string[] dependencies = AssetDatabase.GetDependencies(path, true);
            bool isUsed = false;
            foreach (string otherGuid in allAssets)
            {
                string otherPath = AssetDatabase.GUIDToAssetPath(otherGuid);
                if (otherPath == path) continue;

                string[] otherDependencies = AssetDatabase.GetDependencies(otherPath, true);
                foreach (var dep in otherDependencies)
                {
                    if (dep == path)
                    {
                        isUsed = true;
                        break;
                    }
                }
                if (isUsed) break;
            }

            if (!isUsed)
            {
                unusedAssets.Add(path);
            }
        }

        Debug.Log($"Found {unusedAssets.Count} unused assets:\n" + string.Join("\n", unusedAssets));

    }
}
