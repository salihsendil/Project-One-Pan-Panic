using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CustomizationData))]
public class CustomizationDataDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 6 field var -> her biri + spacing
        int lineCount = 6;
        return (EditorGUIUtility.singleLineHeight + 4) * lineCount;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = 4;

        Rect rect = new Rect(position.x, position.y, position.width, lineHeight);

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("BodyPart"));
        rect.y += lineHeight + spacing;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("Id"));
        rect.y += lineHeight + spacing;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("Mesh"));
        rect.y += lineHeight + spacing;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("BodyColorMaterial"));
        rect.y += lineHeight + spacing;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("FaceMaterial"));
        rect.y += lineHeight + spacing;

        EditorGUI.PropertyField(rect, property.FindPropertyRelative("Cost"));

        EditorGUI.EndProperty();
    }
}