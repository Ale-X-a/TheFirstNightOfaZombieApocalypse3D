using UnityEngine;
using UnityEditor;
using System.IO;

public class PSDCleaner: EditorWindow
{
    private string targetFolder = "Assets/Versatile Studio Assets";

    [MenuItem("Tools/Clean PSD Files")]
    public static void ShowWindow()
    {
        GetWindow<PSDCleaner>("PSD Cleaner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Delete All .psd Files", EditorStyles.boldLabel);
        targetFolder = EditorGUILayout.TextField("Target Folder", targetFolder);

        if (GUILayout.Button("Delete .psd Files"))
        {
            DeletePsdFiles();
        }
    }

    private void DeletePsdFiles()
    {
        string[] files = Directory.GetFiles(targetFolder, "*.psd", SearchOption.AllDirectories);

        int deletedCount = 0;
        foreach (string file in files)
        {
            string assetPath = file.Replace(Application.dataPath, "Assets").Replace("\\", "/");
            if (AssetDatabase.DeleteAsset(assetPath))
            {
                deletedCount++;
            }
        }

        AssetDatabase.Refresh();
        Debug.Log($"Deleted {deletedCount} .psd files from {targetFolder}");
    }
}