using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CustomizationData))]
public class CustomizationDataDrawer : PropertyDrawer
{
    private static float LineHeight = EditorGUIUtility.singleLineHeight;
    private const float VerticalSpacing = 2f;

    // Inspector state'ini property path'e göre tutmak için
    private static readonly Dictionary<string, ToggleState> ToggleStates = new();

    private class ToggleState
    {
        public bool IsMesh;
        public bool IsMaterial;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Alt alanlara eriþim
        SerializedProperty bodyPartProp = property.FindPropertyRelative("BodyPart");
        SerializedProperty idProp = property.FindPropertyRelative("Id");
        SerializedProperty meshProp = property.FindPropertyRelative("Mesh");
        SerializedProperty bodyColorMatProp = property.FindPropertyRelative("BodyColorMaterial");
        SerializedProperty faceMatProp = property.FindPropertyRelative("FaceMaterial");
        SerializedProperty costProp = property.FindPropertyRelative("Cost");

        // Bu property için toggle state al
        if (!ToggleStates.TryGetValue(property.propertyPath, out var state))
        {
            state = new ToggleState();
            ToggleStates[property.propertyPath] = state;
        }

        // Satýr dikey offset hesabý
        Rect row = new Rect(
            position.x,
            position.y,
            position.width,
            LineHeight
        );

        // 1) BodyPart
        EditorGUI.PropertyField(row, bodyPartProp);
        row.y += LineHeight + VerticalSpacing;

        // 2) Id
        EditorGUI.PropertyField(row, idProp);
        row.y += LineHeight + VerticalSpacing;

        // 3) Toggle satýrý (isMesh, isMaterial)
        Rect leftToggleRect = new Rect(row.x, row.y, row.width * 0.5f, LineHeight);
        Rect rightToggleRect = new Rect(row.x + row.width * 0.5f, row.y, row.width * 0.5f, LineHeight);

        state.IsMesh = EditorGUI.ToggleLeft(leftToggleRect, "Mesh", state.IsMesh);
        state.IsMaterial = EditorGUI.ToggleLeft(rightToggleRect, "Material", state.IsMaterial);
        row.y += LineHeight + VerticalSpacing;

        // 4) Mesh alaný (isMesh seçiliyse)
        if (state.IsMesh)
        {
            EditorGUI.PropertyField(row, meshProp);
            row.y += LineHeight + VerticalSpacing;
        }

        // 5) Material alanlarý (isMaterial seçiliyse)
        if (state.IsMaterial)
        {
            EditorGUI.PropertyField(row, bodyColorMatProp);
            row.y += LineHeight + VerticalSpacing;

            EditorGUI.PropertyField(row, faceMatProp);
            row.y += LineHeight + VerticalSpacing;
        }

        // 6) Cost (her zaman)
        EditorGUI.PropertyField(row, costProp);
        row.y += LineHeight + VerticalSpacing;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Maksimum yüksekliði býrakýyoruz; gizlenen alanlar boþluk olarak kalacak
        // (daha karmaþýk hesaplama istersen burayý ToggleStates ile dinamikleþtirebilirsin)
        int lines = 1   // BodyPart
                    + 1 // Id
                    + 1 // toggle satýrý
                    + 1 // Mesh satýrý (varsayýlan olarak yer aç)
                    + 2 // Material satýrlarý
                    + 1; // Cost

        return lines * (LineHeight + VerticalSpacing);
    }
}