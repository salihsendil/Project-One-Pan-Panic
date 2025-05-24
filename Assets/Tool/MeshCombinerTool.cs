using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshCombinerTool : EditorWindow
{
    [Header("Combine Items")]
    [Tooltip("Add items want to combine mesh.")]
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] private List<MeshFilter> meshFilters = new List<MeshFilter>();
    [SerializeField] private Vector2 scrollPos;
    [SerializeField] private bool isIncludeInactive;

    [MenuItem("Window/Salii Extensions/Mesh Combiner Tool")]
    public static void ShowWindow()
    {
        GetWindow<MeshCombinerTool>("Mesh Combiner Tool - By Salii");
    }

    private void Awake()
    {

    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("GameObject Listesi", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(200));

        for (int i = 0; i < gameObjects.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            gameObjects[i] = (GameObject)EditorGUILayout.ObjectField(gameObjects[i], typeof(GameObject), true);
            if (GUILayout.Button("Sil", GUILayout.Width(40)))
            {
                gameObjects.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Is Include Inactive Childs?", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("When adding an object, do you want the Mesh Filters owned by inactive children to be included?", EditorStyles.wordWrappedLabel);
        EditorGUILayout.EndVertical();
        isIncludeInactive = EditorGUILayout.Toggle(isIncludeInactive);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Yeni GameObject Ekle"))
        {
            gameObjects.Add(null);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Oluþtur"))
        {
            GenerateMesh();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Temizle"))
        {
            ClearLists();
        }

        EditorGUILayout.EndHorizontal();
    }
    private void GenerateMesh()
    {
        meshFilters.Clear();

        foreach (var go in gameObjects)
        {
            if (go == null) continue;

            MeshFilter[] filters = go.GetComponentsInChildren<MeshFilter>(isIncludeInactive);
            meshFilters.AddRange(filters);
        }

        if (meshFilters.Count == 0)
        {
            Debug.LogWarning("Hiç mesh bulunamadý.");
            return;
        }

        List<CombineInstance> combine = new List<CombineInstance>();

        foreach (var mf in meshFilters)
        {
            if (mf.sharedMesh == null) continue;

            CombineInstance ci = new CombineInstance();
            ci.mesh = mf.sharedMesh;
            ci.transform = gameObjects[0].transform.worldToLocalMatrix * mf.transform.localToWorldMatrix;
            combine.Add(ci);
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // Büyük mesh'ler için
        combinedMesh.CombineMeshes(combine.ToArray());

        
        GameObject combinedObject = new GameObject("Combined Mesh");
        MeshFilter mfCombined = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer mrCombined = combinedObject.AddComponent<MeshRenderer>();

        mfCombined.sharedMesh = combinedMesh;

        
        foreach (var mf in meshFilters)
        {
            var renderer = mf.GetComponent<MeshRenderer>();
            if (renderer != null && renderer.sharedMaterial != null)
            {
                mrCombined.sharedMaterial = renderer.sharedMaterial;
                break;
            }
        }

        Undo.RegisterCreatedObjectUndo(combinedObject, "Create Combined Mesh");
        Selection.activeGameObject = combinedObject;

        
        string folderPath = "Assets/TestMeshes";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "TestMeshes");
        }

        string assetPath = $"{folderPath}/CombinedMesh_{System.DateTime.Now:yyyyMMdd_HHmmss}.asset";
        AssetDatabase.CreateAsset(combinedMesh, assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Mesh birleþtirme tamamlandý! Toplam: {meshFilters.Count}\nKaydedildi: {assetPath}");
    }



    private void ClearLists()
    {
        gameObjects.Clear();
        meshFilters.Clear();
    }

}
