using UnityEditor;
using UnityEngine;
using System.IO;

public class StaticMemoryAnalyzer
{
    private const string TARGET_FOLDER = "Assets/Scripts";

    [MenuItem("Tools/Static Memory Analyzer With Lines")]
    public static void Analyze()
    {
        if (!Directory.Exists(TARGET_FOLDER))
        {
            Debug.LogError($"Folder not found: {TARGET_FOLDER}");
            return;
        }

        var files = Directory.GetFiles(TARGET_FOLDER, "*.cs", SearchOption.AllDirectories);

        int totalScripts = files.Length;
        int monoBehaviourCount = 0;
        int updateCount = 0;
        int allocRiskCount = 0;

        Debug.Log("📌 Scripts with Update method or potential allocations:");

        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);
            string content = string.Join("\n", lines);

            bool isMono = content.Contains(": MonoBehaviour");
            if (isMono) monoBehaviourCount++;

            bool hasUpdate = content.Contains("void Update()");
            if (hasUpdate) updateCount++;

            bool fileHasAlloc = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                // Potansiyel allocation check
                if (line.Contains("new ") || line.Contains(".ToList()")
                    || line.Contains(".Where(") || line.Contains(".Select("))
                {
                    if (!fileHasAlloc)
                    {
                        fileHasAlloc = true;
                        allocRiskCount++;
                        Debug.LogError($"⚠ Potential allocation in {file}");
                    }

                    Debug.Log($"   Line {i + 1}: {line}");
                }
            }

            if (hasUpdate || fileHasAlloc)
            {
                string logMsg = $"- {file}";
                if (hasUpdate) logMsg += " [Update]";
                if (fileHasAlloc) logMsg += " [Potential Alloc]";
                Debug.Log(logMsg);
            }
        }

        Debug.Log("--------------------------------------------------");
        Debug.Log($"📄 Total Scripts: {totalScripts}");
        Debug.Log($"🎮 MonoBehaviour Scripts: {monoBehaviourCount}");
        Debug.Log($"🔄 Scripts with Update(): {updateCount}");
        Debug.Log($"⚠ Scripts with potential allocations: {allocRiskCount}");
    }
}