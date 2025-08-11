using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Linq;
public enum ColliderType
{
    None,
    BoxCollider,
    SphereCollider,
    CapsuleCollider,
    MeshCollider
}

[System.Serializable]
public struct PrefabData
{
    public GameObject prefab;
    public ColliderType colliderType;

    public PrefabData(GameObject prefab, ColliderType colliderType)
    {
        this.prefab = prefab;
        this.colliderType = colliderType;
    }
}

public class ObjectPlacerTool : EditorWindow
{
    [Header("Editor Settings")]
    private Vector2 scrollPos;

    [Header("Parent Object")]
    private string parentObjectName = "Editor Placed Objects";
    private GameObject parentObject;

    [Header("Prefab")]
    private SerializedObject serializedObject;
    private SerializedProperty serializedPrefabsProperty;
    [SerializeField] private List<PrefabData> prefabs = new List<PrefabData>();
    private int prefabCount = 10;
    private float heightOffset = 0f;

    [Header("Brush Options")]
    [Range(0.1f, 100f)]
    private float brushSize = 5f;

    [Header("Rotation Settings")]
    private bool canRotateX = true;
    private bool canRotateY = true;
    private bool canRotateZ = true;

    [Header("Tag Options")]
    private int selectedTagIndex = 0;
    private string selectedTag;
    private string tagNameToAdd;

    [Header("Layer Options")]
    private int selectedLayerIndex = 0;


    [MenuItem("Tools/Object Placer Tool")]
    private static void ShowWindow()
    {
        GetWindow<ObjectPlacerTool>("Object Placer - By Salii");
    }

    private void OnGUI()
    {
        #region GlobalStyles

        GUIStyle boldLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        boldLabelStyle.wordWrap = true;

        GUIStyle toggleLabelStyle = new GUIStyle(EditorStyles.label);
        toggleLabelStyle.alignment = TextAnchor.MiddleLeft;
        toggleLabelStyle.fontStyle = FontStyle.Bold;
        toggleLabelStyle.normal.textColor = Color.white;

        GUIStyle toggleButtonStyle = new GUIStyle(GUI.skin.toggle);
        toggleButtonStyle.margin = new RectOffset(0, 10, 0, 0);

        #endregion

        EditorGUILayout.BeginHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

        #region Parent Object GUI

        SectionHeader("Parent Object Settings");

        //Parent Object GUI
        EditorGUILayout.Space(2);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent Object", boldLabelStyle, GUILayout.Width(150));
        parentObject = (GameObject)EditorGUILayout.ObjectField(parentObject, typeof(GameObject), true, GUILayout.ExpandWidth(true));
        EditorGUILayout.EndHorizontal();

        //Parent Object Name GUI
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Parent Object Name", boldLabelStyle, GUILayout.Width(150));
        if (parentObject != null)
        {
            parentObjectName = EditorGUILayout.TextField(parentObject.name, GUILayout.ExpandWidth(true));
        }
        else
        {
            parentObjectName = EditorGUILayout.TextField(parentObjectName, GUILayout.ExpandWidth(true));
        }
        EditorGUILayout.EndHorizontal();

        #endregion

        #region Prefab Settings GUI

        SectionHeader("Prefab Settings");

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginHorizontal();
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedPrefabsProperty, true);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number Of Clone", boldLabelStyle, GUILayout.Width(120));
        prefabCount = EditorGUILayout.IntField(prefabCount, GUILayout.Width(50));

        EditorGUILayout.Space(2);

        EditorGUILayout.LabelField("Height Offset", boldLabelStyle, GUILayout.Width(100));
        heightOffset = EditorGUILayout.FloatField(heightOffset, GUILayout.Width(50));
        EditorGUILayout.EndHorizontal();

        #endregion

        #region Layer Options GUI

        SectionHeader("Layer Options");

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Target Layer", boldLabelStyle, GUILayout.Width(100));
        selectedLayerIndex = EditorGUILayout.Popup(selectedLayerIndex, InternalEditorUtility.layers);
        int selectedLayer = LayerMask.NameToLayer(InternalEditorUtility.layers[selectedLayerIndex]);

        EditorGUILayout.EndHorizontal();

        #endregion

        #region Brush Settings GUI

        SectionHeader("Brush Settings");

        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Brush Size", boldLabelStyle, GUILayout.Width(150));
        brushSize = EditorGUILayout.Slider(brushSize, 0.1f, 100f, GUILayout.ExpandWidth(true), GUILayout.MinWidth(150));
        EditorGUILayout.EndHorizontal();

        #endregion

        #region Random Rotation GUI

        SectionHeader("Rotation Settings");

        // Random Rotation GUI //
        EditorGUILayout.Space(2);
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Random Rotation On:", boldLabelStyle, GUILayout.Width(150));

        // X Toggle
        GUILayout.Label("X", toggleLabelStyle, GUILayout.Width(15));
        canRotateX = GUILayout.Toggle(canRotateX, "", toggleButtonStyle, GUILayout.Width(20));

        // Y Toggle
        GUILayout.Label("Y", toggleLabelStyle, GUILayout.Width(15));
        canRotateY = GUILayout.Toggle(canRotateY, "", toggleButtonStyle, GUILayout.Width(20));

        // Z Toggle
        GUILayout.Label("Z", toggleLabelStyle, GUILayout.Width(15));
        canRotateZ = GUILayout.Toggle(canRotateZ, "", toggleButtonStyle, GUILayout.Width(20));

        EditorGUILayout.EndHorizontal();

        #endregion

        #region Tag Option GUI

        SectionHeader("Tag Settings");

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Object Tag", boldLabelStyle, GUILayout.Width(150));
        selectedTagIndex = EditorGUILayout.Popup(selectedTagIndex, InternalEditorUtility.tags);
        selectedTag = InternalEditorUtility.tags[selectedTagIndex];
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space(2);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tag Name", boldLabelStyle, GUILayout.Width(150));
        tagNameToAdd = EditorGUILayout.TextField(tagNameToAdd, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Add Tag"))
        {
            AddGameObjectTag();
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        #region Buttons

        EditorGUILayout.Space(15);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Parent Object Test"))
        {
            CheckParentObjectOnHierarchy();
        }

        if (GUILayout.Button("Clear Values"))
        {
            ClearValues();
        }

        if (GUILayout.Button("Delete Objects By Tag"))
        {
            DeleteObjectsByTag();
        }

        EditorGUILayout.EndHorizontal();

        #endregion

        #region Github Button
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Go to Github"))
        {
            Application.OpenURL("https://github.com/salihsendil/Unity-Environment-Placer-Tool");
        }

        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, GetMaskedLayer()))
        {
            Handles.color = Color.green;
            Handles.CubeHandleCap(0, hit.point, Quaternion.identity, 0.2f, EventType.Repaint);
            Handles.DrawWireDisc(hit.point, Vector3.up, brushSize);
        }

        if ((e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0)
        {
            LeftMouseButtonClicked();
        }

        if ((e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 1)
        {
            RightMouseButtonClicked();
        }

        sceneView.Repaint();
    }

    private void DeleteObjectsByTag()
    {
        if (selectedTag == null)
        {
            Debug.LogWarning("Tag Name can not empty. Please enter Tag Name!");
            return;
        }

        if (!InternalEditorUtility.tags.Contains(selectedTag))
        {
            Debug.LogError("No tag with this name was found. Please enter a valid tag name.");
            return;
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag(selectedTag);
        if (objects.Length == 0)
        {
            Debug.LogWarning("No objects found with the tag: " + selectedTag);
            return;
        }

        int group = Undo.GetCurrentGroup();
        Undo.SetCurrentGroupName("Delete Objects with Tag: " + selectedTag);

        foreach (var obj in objects)
        {
            Undo.DestroyObjectImmediate(obj);
        }

        Undo.CollapseUndoOperations(group);

    }

    private void LeftMouseButtonClicked()
    {
        Event e = Event.current;

        Ray mousePosRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        if (Physics.Raycast(mousePosRay, out RaycastHit mouseRayHit, 100f, GetMaskedLayer()))
        {
            CheckParentObjectOnHierarchy();

            for (int i = 0; i < prefabCount; i++)
            {
                Ray posRay = new Ray(mouseRayHit.point + Vector3.up * 100f + GetRandomPosition(), Vector3.down);
                if (Physics.Raycast(posRay, out RaycastHit posRayHit, Mathf.Infinity, GetMaskedLayer()))
                {
                    InstantiatePrefab(posRayHit.point + Vector3.up * heightOffset);
                }
            }
        }

        e.Use();
    }


    private void RightMouseButtonClicked()
    {
        Event e = Event.current;

        Ray mousePosRay = HandleUtility.GUIPointToWorldRay(e.mousePosition);

        if (Physics.Raycast(mousePosRay, out RaycastHit mouseRayHit, 100f, GetMaskedLayer()))
        {
            Ray posRay = new Ray(mouseRayHit.point + Vector3.up * 100f, Vector3.down);

            RaycastHit[] hits = Physics.SphereCastAll(posRay, brushSize, Mathf.Infinity);

            int group = Undo.GetCurrentGroup();
            Undo.SetCurrentGroupName("Delete Objects with Tag: " + selectedTag);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag(selectedTag))
                {
                    Undo.DestroyObjectImmediate(hit.collider.gameObject);
                }
            }

            Undo.CollapseUndoOperations(group);
        }

        e.Use();

    }

    private void InstantiatePrefab(Vector3 position)
    {
        if (prefabs.Count > 0 && parentObject != null)
        {
            PrefabData data = GetRandomPrefabData();
            GameObject gameObject = (GameObject)PrefabUtility.InstantiatePrefab(data.prefab, parentObject.transform);
            gameObject.transform.rotation = GetRandomRotation(canRotateX, canRotateY, canRotateZ);
            gameObject.transform.position = position;

            if (gameObject.GetComponent<Collider>() == null)
            {
                SetColliderType(gameObject, data.colliderType);
            }
            gameObject.tag = selectedTag;
            Undo.RegisterCreatedObjectUndo(gameObject, "Place Prefab");
        }

        else
        {
            Debug.LogWarning("Prefab Null! Please select prefab.");
            return;
        }
    }

    private void CheckParentObjectOnHierarchy()
    {
        parentObject = GameObject.Find(parentObjectName);

        if (parentObject != null) { return; }

        if (string.IsNullOrWhiteSpace(parentObjectName)) { Debug.LogError("Please enter parent object name!"); return; }

        GameObject gameObject = new GameObject(parentObjectName);
        parentObject = gameObject;
        Debug.Log("There were not any Parent object. Created one. Named: " + parentObjectName);

    }

    private PrefabData GetRandomPrefabData()
    {
        PrefabData data = new PrefabData();

        if (prefabs.Count > 0)
        {
            data = prefabs[Random.Range(0, prefabs.Count)];
            return data;
        }

        else
        {
            Debug.LogError("Prefab list is null. Please add at least one(1) prefab");
            return data;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomPos = Random.insideUnitCircle * brushSize;
        return new Vector3(randomPos.x, 0f, randomPos.y);
    }

    private Quaternion GetRandomRotation(bool X, bool Y, bool Z)
    {
        float randomX = X ? Random.Range(0, 360) : 0;
        float randomY = Y ? Random.Range(0, 360) : 0;
        float randomZ = Z ? Random.Range(0, 360) : 0;
        Vector3 rotVector = new Vector3(randomX, randomY, randomZ);

        return Quaternion.Euler(rotVector);
    }

    private void AddGameObjectTag()
    {
        if (!InternalEditorUtility.tags.Contains(tagNameToAdd))
        {
            if (string.IsNullOrWhiteSpace(tagNameToAdd))
            {
                Debug.LogError("Please enter valid tag name!");
                return;
            }

            InternalEditorUtility.AddTag(tagNameToAdd);
            Debug.Log($"Tag '{tagNameToAdd}' added successfully!");
        }

        else
        {
            Debug.Log($"Tag '{tagNameToAdd}' already exists.");
        }
    }

    private void SetColliderType(GameObject obj, ColliderType colliderType)
    {
        switch (colliderType)
        {
            case ColliderType.BoxCollider:
                obj.AddComponent<BoxCollider>();
                break;
            case ColliderType.SphereCollider:
                obj.AddComponent<SphereCollider>();
                break;
            case ColliderType.CapsuleCollider:
                obj.AddComponent<CapsuleCollider>();
                break;
            case ColliderType.MeshCollider:
                obj.AddComponent<MeshCollider>();
                break;
            default:
                var box = obj.AddComponent<BoxCollider>();
                box.isTrigger = true;
                box.hideFlags = HideFlags.DontSaveInBuild | HideFlags.NotEditable;
                break;
        }
    }

    private int GetMaskedLayer()
    {
        int layerIndex = LayerMask.NameToLayer(InternalEditorUtility.layers[selectedLayerIndex]);
        return 1 << layerIndex;
    }

    private void ClearValues()
    {
        parentObjectName = "";
        parentObject = null;
        prefabs = null;
        prefabCount = 10;
        heightOffset = 0f;
        brushSize = 5f;
        canRotateX = true;
        canRotateY = true;
        canRotateZ = true;
        tagNameToAdd = "";
        selectedLayerIndex = 0;
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;

        serializedObject = new SerializedObject(this);
        serializedPrefabsProperty = serializedObject.FindProperty("prefabs");
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void SectionHeader(string header)
    {
        GUIStyle headerStyle = new GUIStyle(EditorStyles.label);
        headerStyle.alignment = TextAnchor.MiddleCenter;
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.normal.textColor = Color.white;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField(header, headerStyle, GUILayout.ExpandWidth(true));
    }
}