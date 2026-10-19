using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class RecipeExporterTool : EditorWindow
{
    // Sürükle-býrak ile doldurulabilecek RecipeSO listesi
    private List<RecipeSO> recipeList = new List<RecipeSO>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/Recipe Exporter Tool")]
    public static void ShowWindow()
    {
        GetWindow<RecipeExporterTool>("Recipe Exporter");
    }

    private void OnGUI()
    {
        GUILayout.Label("RecipeSO Exporter", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        // Liste için ReorderableList veya standart SerializedObject yerine basit bir listeleme kullanýyoruz
        // Sürükle-býrak desteði için SerializedObject yaklaþýmý daha kararlýdýr.
        ScriptableObject targetObj = this;
        SerializedObject so = new SerializedObject(targetObj);

        // Alternatif olarak standart Unity GUI ile liste yönetimi:
        DrawRecipeListGUI();

        EditorGUILayout.Space(10);

        // Butonlar Yan Yana
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Listeyi Temizle", GUILayout.Height(30)))
        {
            recipeList.Clear();
        }

        if (GUILayout.Button("TXT Oluþtur", GUILayout.Height(30)))
        {
            ExportRecipesToTxt();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawRecipeListGUI()
    {
        EditorGUILayout.LabelField("RecipeSO Listesi", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(250));

        // Listeye eleman ekleme/çýkarma ve sürükleme alaný
        int newCount = Mathf.Max(0, EditorGUILayout.IntField("Boyut", recipeList.Count));
        while (newCount < recipeList.Count) recipeList.RemoveAt(recipeList.Count - 1);
        while (newCount > recipeList.Count) recipeList.Add(null);

        for (int i = 0; i < recipeList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            recipeList[i] = (RecipeSO)EditorGUILayout.ObjectField($"Reçete {i + 1}", recipeList[i], typeof(RecipeSO), false);
            EditorGUILayout.EndHorizontal();
        }

        // Boþluða sürükle-býrak yapabilmek için alan
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 30.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "RecipeSO Dosyalarýný Buraya Sürükleyin");
        EventType eventType = Event.current.type;

        if ((eventType == EventType.DragUpdated || eventType == EventType.DragPerform) && dropArea.Contains(Event.current.mousePosition))
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (eventType == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();
                foreach (Object draggedObject in DragAndDrop.objectReferences)
                {
                    if (draggedObject is RecipeSO recipeSO)
                    {
                        if (!recipeList.Contains(recipeSO))
                        {
                            recipeList.Add(recipeSO);
                        }
                    }
                }
            }
            Event.current.Use();
        }

        EditorGUILayout.EndScrollView();
    }

    private void ExportRecipesToTxt()
    {
        if (recipeList == null || recipeList.Count == 0)
        {
            EditorUtility.DisplayDialog("Hata", "Dýþa aktarýlacak hiç RecipeSO bulunamadý!", "Tamam");
            return;
        }

        // Kaydetme paneli aç
        string path = EditorUtility.SaveFilePanel("TXT Olarak Kaydet", "", "RecipesExport.txt", "txt");
        if (string.IsNullOrEmpty(path)) return;

        StringBuilder sb = new StringBuilder();

        foreach (var recipe in recipeList)
        {
            if (recipe == null) continue;

            // recipeSO.name
            sb.AppendLine(recipe.name);

            if (recipe.Ingredients != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    string ingredientName = ingredient.IngredientSO != null ? ingredient.IngredientSO.name : "Bilinmeyen Malzeme";
                    string stageName = ingredient.RequiredStage.ToString();

                    // -ingredientso.name, requiredStage
                    sb.AppendLine($"-{ingredientName}, {stageName}");
                }
            }
            sb.AppendLine(); // Tarifler arasý boþluk
        }

        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Baþarýlý", "TXT dosyasý baþarýyla oluþturuldu!", "Tamam");
        Debug.Log($"Reçeteler baþarýyla dýþa aktarýldý: {path}");
    }
}