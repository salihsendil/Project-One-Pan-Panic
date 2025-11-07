using UnityEditor;

[CustomEditor(typeof(UIActionButtonSO))]
public class ActionButtonSOEditorDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty actionType = serializedObject.FindProperty("actionType");
        EditorGUILayout.PropertyField(actionType);

        // Enum deðerini al
        UIButtonActionType type = (UIButtonActionType)actionType.enumValueIndex;

        // Þartlý olarak alanlarý göster
        switch (type)
        {
            case UIButtonActionType.LoadScene:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("scene"));
                break;

            case UIButtonActionType.ToggleSound or UIButtonActionType.ToggleMusic:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("toggleOnSprite"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("toggleOffSprite"));
                break;

            case UIButtonActionType.QuitGame:
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}