using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public class CodeAnalyzerTool
{
    private const string TARGET_FOLDER = "Assets/Scripts";

    [MenuItem("Tools/Analyze Scripts With Update")]
    public static void Analyze()
    {
        if (!Directory.Exists(TARGET_FOLDER))
        {
            Debug.LogError($"Folder not found: {TARGET_FOLDER}");
            return;
        }

        var files = Directory.GetFiles(TARGET_FOLDER, "*.cs", SearchOption.AllDirectories);

        int totalLines = 0;
        int monoBehaviourCount = 0;
        int updateMethodCount = 0;
        int nonEmptyUpdateCount = 0;

        Debug.Log("📌 Scripts with Update method:");

        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);
            totalLines += lines.Length;

            string content = string.Join("\n", lines);

            // MonoBehaviour kontrolü
            bool isMono = content.Contains(": MonoBehaviour");
            if (isMono)
                monoBehaviourCount++;

            // Update method kontrolü
            if (content.Contains("void Update()"))
            {
                updateMethodCount++;

                // Basit doluluk kontrolü
                int startIndex = content.IndexOf("void Update()");
                int braceOpen = content.IndexOf("{", startIndex);
                int braceClose = content.IndexOf("}", braceOpen);

                if (braceOpen != -1 && braceClose != -1)
                {
                    string body = content.Substring(braceOpen + 1, braceClose - braceOpen - 1);

                    if (!string.IsNullOrWhiteSpace(body) && body.Trim() != "")
                    {
                        nonEmptyUpdateCount++;
                    }
                }

                // Script dosya yolu ve ismi
                Debug.Log($"  - {file}");
            }
        }

        Debug.Log("--------------------------------------------------");
        Debug.Log($"📁 Folder: {TARGET_FOLDER}");
        Debug.Log($"📄 Total Scripts: {files.Length}");
        Debug.Log($"🧮 Total Lines: {totalLines}");
        Debug.Log($"🎮 MonoBehaviour Scripts: {monoBehaviourCount}");
        Debug.Log($"🔄 Scripts with Update(): {updateMethodCount}");
        Debug.Log($"⚡ Non-empty Update(): {nonEmptyUpdateCount}");
    }
}