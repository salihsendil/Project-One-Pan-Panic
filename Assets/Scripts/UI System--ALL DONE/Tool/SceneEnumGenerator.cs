using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public static class SceneEnumGenerator
{
    private const string EnumName = "ScenesEnum";
    private const string FilePath = "Assets/Scripts/UI System/Enums/ScenesEnum.cs";

    [MenuItem("Tools/Generate Scenes Enum")]
    public static void Generate()
    {
        // Build Settings'teki sahneleri al
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => Path.GetFileNameWithoutExtension(s.path))
            .ToArray();

        // Dosya dizini yoksa oluþtur
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        using (StreamWriter writer = new StreamWriter(FilePath, false))
        {
            writer.WriteLine("public enum " + EnumName);
            writer.WriteLine("{");

            foreach (var scene in scenes)
            {
                // Boþluk veya geçersiz karakter varsa temizle
                string cleanName = scene.Replace(" ", "_").Replace("-", "_");
                writer.WriteLine($"    {cleanName},");
            }

            writer.WriteLine("}");
        }

        AssetDatabase.Refresh();
        Debug.Log($" {EnumName} baþarýyla oluþturuldu: {FilePath}");
    }
}
