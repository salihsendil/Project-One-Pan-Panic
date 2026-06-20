using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class TutorialExporter : EditorWindow
{
    private TutorialStepSO targetSO;

    // Unity üst menüsüne aracý ekliyoruz
    [MenuItem("Tools/Tutorial Description Exporter")]
    public static void ShowWindow()
    {
        GetWindow<TutorialExporter>("Tutorial Exporter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tutorial Description Exporter", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // ScriptableObject'i sürükleyip býrakacaðýmýz alan
        targetSO = (TutorialStepSO)EditorGUILayout.ObjectField("Target Tutorial SO", targetSO, typeof(TutorialStepSO), false);

        EditorGUILayout.Space();

        // Eðer SO seçildiyse butonu aktif et
        if (targetSO != null)
        {
            if (GUILayout.Button("Export Descriptions to TXT", GUILayout.Height(40)))
            {
                ExportToTxt();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Lütfen iþlem yapmak için bir TutorialStepSO dosyasý sürükleyin.", MessageType.Info);
        }
    }

    private void ExportToTxt()
    {
        if (targetSO.Steps == null || targetSO.Steps.Count == 0)
        {
            Debug.LogWarning("Seçilen ScriptableObject'in içinde hiç adým (Step) bulunamadý!");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"--- {targetSO.name} Description Export ---");
        sb.AppendLine($"Toplam Adým Sayýsý: {targetSO.Steps.Count}");
        sb.AppendLine("-------------------------------------------\n");

        int counter = 1;
        foreach (var step in targetSO.Steps)
        {
            sb.AppendLine($"[Adým {counter}]");

            // NOT: Eðer senin TutorialStep sýnýfýndaki deðiþkenin adý Dialogue deðil de 
            // "description" ise aþaðýdaki "step.Dialogue" kýsmýný "step.description" yap kanka.
            string text = !string.IsNullOrEmpty(step.Dialogue) ? step.Dialogue : "[BOÞ DIALOGUE]";

            sb.AppendLine(text);
            sb.AppendLine(); // Adýmlar arasýnda bir satýr boþluk býrak
            counter++;
        }

        // Kullanýcýya dosyayý nereye kaydedeceðini soran panel açýlýyor (Varsayýlan: Masaüstü)
        string defaultFileName = $"{targetSO.name}_Descriptions";
        string path = EditorUtility.SaveFilePanel("Save Tutorial Text", "", defaultFileName, "txt");

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            Debug.Log($"<color=green>Baþarýlý!</color> Tüm tutorial metinleri þuraya kaydedildi: {path}");

            // Ýþlem bitince dosyayý otomatik aç (isteðe baðlý)
            EditorUtility.RevealInFinder(path);
        }
    }
}