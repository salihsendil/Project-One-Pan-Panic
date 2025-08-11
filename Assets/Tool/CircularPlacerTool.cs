using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircularPlacerTool : EditorWindow
{
    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;
    private Vector2 scrollPos;

    [SerializeField] private List<GameObject> gameobjects = new List<GameObject>();
    private Vector3 origin = new Vector3();
    private float radius = 1f;


    [MenuItem("Tools/Circular Placer")]
    private static void ShowWindow()
    {
        GetWindow<CircularPlacerTool>("Circular Placer - By Salii");
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedProperty);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(3);
        EditorGUILayout.BeginHorizontal();
        radius = EditorGUILayout.FloatField("Radius ", radius);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(3);
        EditorGUILayout.BeginHorizontal();
        origin = EditorGUILayout.Vector3Field("Origin ", origin);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(3);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Place Objects"))
        {
            PlaceAndRotateObjects();
        }

        if (GUILayout.Button("Clear Values"))
        {
            ClearValues();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(12);
        EditorGUILayout.EndScrollView();
    }
    private void PlaceAndRotateObjects()
    {
        if (gameobjects == null) { Debug.LogError("Gameobjects can not null!"); return; }

        float angle = 360 / gameobjects.Count;

        int group = Undo.GetCurrentGroup();
        Undo.SetCurrentGroupName("Object Placed!");

        for (int i = 0; i < gameobjects.Count; i++)
        {
            float xPos = radius * Mathf.Cos(angle * i * Mathf.Deg2Rad);
            float zPos = radius * Mathf.Sin(angle * i * Mathf.Deg2Rad);

            gameobjects[i].transform.position = origin + new Vector3(xPos, 0f, zPos);
            gameobjects[i].transform.LookAt(origin);
        }

        Undo.CollapseUndoOperations(group);
        Debug.Log("All objects were placed at a radius of " + radius + " units from the origin point at " + origin + " and rotated by " + angle + " degrees each.");

    }

    private void ClearValues()
    {
        gameobjects = null;
        origin = Vector3.zero;
        radius = 1f;
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        serializedProperty = serializedObject.FindProperty("gameobjects");
    }
}
