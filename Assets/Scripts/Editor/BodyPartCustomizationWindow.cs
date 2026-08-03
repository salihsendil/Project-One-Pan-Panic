using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BodyPartCustomizationWindow : EditorWindow
{
    private BodyPartCustomizationSO targetSO;
    private BodyPartType selectedBodyPart = BodyPartType.Body;

    // Hedef Listeler
    public List<Material> materialsList = new List<Material>();
    public List<Mesh> meshesList = new List<Mesh>();

    // Ekonomi Ayarlarý
    private int startCost = 100;
    private int increaseAmount = 50;
    private int useTime = 3;

    // Scroll ve SerializedObject Deðiþkenleri
    private Vector2 scrollPos;
    private SerializedObject serializedObject;
    private SerializedProperty materialsProperty;
    private SerializedProperty meshesProperty;

    [MenuItem("Tools/Customization Builder")]
    public static void ShowWindow()
    {
        GetWindow<BodyPartCustomizationWindow>("Customization Builder");
    }

    private void OnEnable()
    {
        // Window açýldýðýnda veya derlendiðinde SerializedObject baðlantýsýný kur
        serializedObject = new SerializedObject(this);
        materialsProperty = serializedObject.FindProperty("materialsList");
        meshesProperty = serializedObject.FindProperty("meshesList");
    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            serializedObject = new SerializedObject(this);
            materialsProperty = serializedObject.FindProperty("materialsList");
            meshesProperty = serializedObject.FindProperty("meshesList");
        }

        serializedObject.Update(); // Deðiþiklikleri takip et

        GUILayout.Label("Customization SO Populate Tool", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 1. ScriptableObject Referansý
        targetSO = (BodyPartCustomizationSO)EditorGUILayout.ObjectField("Target SO", targetSO, typeof(BodyPartCustomizationSO), false);

        // 2. Body Part Tipi Seçimi
        selectedBodyPart = (BodyPartType)EditorGUILayout.EnumPopup("Body Part Type", selectedBodyPart);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Economy Parameters", EditorStyles.boldLabel);
        startCost = EditorGUILayout.IntField("Start Cost", startCost);
        increaseAmount = EditorGUILayout.IntField("Increase Amount", increaseAmount);
        useTime = Mathf.Max(1, EditorGUILayout.IntField("Use Time (Repeat Count)", useTime));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Asset List", EditorStyles.boldLabel);

        // Scroll baþlangýcý
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        // Enum seçimine göre listenin çizdirilmesi
        if (selectedBodyPart == BodyPartType.Body || selectedBodyPart == BodyPartType.Face)
        {
            EditorGUILayout.PropertyField(materialsProperty, new GUIContent("Materials"), true);
        }
        else if (selectedBodyPart == BodyPartType.Hat)
        {
            EditorGUILayout.PropertyField(meshesProperty, new GUIContent("Meshes"), true);
        }

        EditorGUILayout.EndScrollView();

        // Deðiþiklikleri seri hale getir
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();

        // 3. SO Doldurma Butonu
        GUI.enabled = targetSO != null;
        if (GUILayout.Button("Populate Customization SO", GUILayout.Height(35)))
        {
            PopulateSO();
        }
        GUI.enabled = true;

        if (targetSO == null)
        {
            EditorGUILayout.HelpBox("Lütfen iþlem yapabilmek için bir Target SO atayýn.", MessageType.Warning);
        }
    }

    private void PopulateSO()
    {
        Undo.RecordObject(targetSO, "Populate Customization SO");
        targetSO.ClothsList.Clear();

        int currentCost = startCost;
        int currentUseCount = 0;

        if (selectedBodyPart == BodyPartType.Body || selectedBodyPart == BodyPartType.Face)
        {
            for (int i = 0; i < materialsList.Count; i++)
            {
                Material mat = materialsList[i];
                if (mat == null) continue;

                CustomizationData data = CreateBaseData(mat.name, currentCost);

                if (selectedBodyPart == BodyPartType.Body)
                {
                    data.BodyColorMaterial = mat;
                }
                else if (selectedBodyPart == BodyPartType.Face)
                {
                    data.FaceMaterial = mat;
                }

                targetSO.ClothsList.Add(data);
                CalculateNextCost(ref currentCost, ref currentUseCount);
            }
        }
        else if (selectedBodyPart == BodyPartType.Hat)
        {
            for (int i = 0; i < meshesList.Count; i++)
            {
                Mesh mesh = meshesList[i];
                if (mesh == null) continue;

                CustomizationData data = CreateBaseData(mesh.name, currentCost);
                data.Mesh = mesh;

                targetSO.ClothsList.Add(data);
                CalculateNextCost(ref currentCost, ref currentUseCount);
            }
        }

        EditorUtility.SetDirty(targetSO);
        AssetDatabase.SaveAssets();

        Debug.Log($"<color=green>[Customization Builder]</color> {targetSO.name} baþarýyla {targetSO.ClothsList.Count} adet veri ile dolduruldu!");
    }

    private CustomizationData CreateBaseData(string rawName, int cost)
    {
        CustomizationData data = new CustomizationData();
        data.BodyPart = selectedBodyPart;
        data.Id = rawName.Trim().Replace(" ", "_");
        data.Cost = cost;
        return data;
    }

    private void CalculateNextCost(ref int currentCost, ref int currentUseCount)
    {
        currentUseCount++;
        if (currentUseCount >= useTime)
        {
            currentCost += increaseAmount;
            currentUseCount = 0;
        }
    }
}