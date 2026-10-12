using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class UnusedAssetsFinder : EditorWindow
{
    [MenuItem("Tools/Kullanýlmayan Asset'leri Tara")]
    public static void ShowWindow()
    {
        GetWindow<UnusedAssetsFinder>("Unused Assets Finder");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Projedeki Baðýmsýz/Kullanýlmayan Varlýklarý Tara", GUILayout.Height(40)))
        {
            FindUnused();
        }
    }

    private void FindUnused()
    {
        // Tüm sahnelerin yollarýný al
        string[] allScenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);

        // Sahnelerin kullandýðý TÜM baðýmlýlýklarý/referanslarý çýkar
        HashSet<string> usedAssets = new HashSet<string>(AssetDatabase.GetDependencies(allScenes, true));

        // Assets klasöründeki her þeyi al
        string[] allAssets = AssetDatabase.FindAssets("", new[] { "Assets" });
        int unusedCount = 0;

        Debug.Log("=== KULLANILMAYAN ASSET/SCRIPT TARAMASI BAÞLADI ===");

        foreach (string guid in allAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            // Klasörleri, Editor scriptlerini ve sahneleri ele
            if (Directory.Exists(path) || path.Contains("/Editor/") || path.EndsWith(".unity"))
                continue;

            // Eðer bu asset sahnelerin baðýmlýlýk aðacýnda YOKSA
            if (!usedAssets.Contains(path))
            {
                Debug.LogWarning($"[Kullanýlmýyor/Referanssýz]: {path}", AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path));
                unusedCount++;
            }
        }

        Debug.Log($"=== TARAMA BÝTTÝ: Toplam {unusedCount} adet kullanýlmayan dosya/script bulundu. ===");
    }
}