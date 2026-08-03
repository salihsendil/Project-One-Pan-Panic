using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialColorComparerWindow : EditorWindow
{
    public List<Material> listA = new List<Material>();
    public List<Material> listB = new List<Material>();

    private SerializedObject serializedObject;
    private SerializedProperty listAProperty;
    private SerializedProperty listBProperty;

    private Vector2 scrollPos;

    [MenuItem("Tools/Material Color Comparer")]
    public static void ShowWindow()
    {
        GetWindow<MaterialColorComparerWindow>("Material Color Comparer");
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        listAProperty = serializedObject.FindProperty("listA");
        listBProperty = serializedObject.FindProperty("listB");
    }

    private void OnGUI()
    {
        if (serializedObject == null)
        {
            serializedObject = new SerializedObject(this);
            listAProperty = serializedObject.FindProperty("listA");
            listBProperty = serializedObject.FindProperty("listB");
        }

        serializedObject.Update();

        GUILayout.Label("Material BaseMap Color Comparer", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.PropertyField(listAProperty, new GUIContent("List A (Source Materials)"), true);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(listBProperty, new GUIContent("List B (Target Materials)"), true);

        EditorGUILayout.EndScrollView();

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();

        GUI.enabled = listA.Count > 0 && listB.Count > 0;
        if (GUILayout.Button("Compare Material Colors", GUILayout.Height(35)))
        {
            CompareMaterials();
        }
        GUI.enabled = true;
    }

    private void CompareMaterials()
    {
        int matchCount = 0;
        Debug.Log("<color=cyan>[Material Color Comparer]</color> Karþýlaþtýrma baþlatýldý...");

        for (int i = 0; i < listA.Count; i++)
        {
            Material matA = listA[i];
            if (matA == null) continue;

            Color colorA = GetBaseColor(matA);
            string hexA = ColorUtility.ToHtmlStringRGBA(colorA);

            for (int j = 0; j < listB.Count; j++)
            {
                Material matB = listB[j];
                if (matB == null) continue;

                Color colorB = GetBaseColor(matB);
                string hexB = ColorUtility.ToHtmlStringRGBA(colorB);

                if (hexA == hexB)
                {
                    matchCount++;
                    Debug.Log($"<color=yellow>Ayný iki material bulundu</color> - <color=orange>{matA.name}</color> (Hex: #{hexA}) deðeri ile <color=orange>{matB.name}</color> (Hex: #{hexB}) deðeri ayný.");
                }
            }
        }

        Debug.Log($"<color=green>[Material Color Comparer]</color> Karþýlaþtýrma bitti. Toplam <b>{matchCount}</b> eþleþme bulundu.");
    }

    /// <summary>
    /// URP, HDRP ve Standard Shader renk parametrelerini otomatik bulur.
    /// </summary>
    private Color GetBaseColor(Material mat)
    {
        if (mat.HasProperty("_BaseColor")) // URP / HDRP Lit
        {
            return mat.GetColor("_BaseColor");
        }
        else if (mat.HasProperty("_Color")) // Built-in Standard / Legacy
        {
            return mat.GetColor("_Color");
        }
        else if (mat.HasProperty("_MainColor")) // Bazý özel Shader'lar
        {
            return mat.GetColor("_MainColor");
        }

        // Renk parametresi bulunamazsa varsayýlan beyaz döner
        return mat.color;
    }
}