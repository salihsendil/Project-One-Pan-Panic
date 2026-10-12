using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TutorialStepSO))]
public class TutorialStepSODrawer : Editor
{
    private SerializedProperty stepList;
    private int lastSelectedIndex = -1;

    private void OnEnable()
    {
        stepList = serializedObject.FindProperty("Steps");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.Space(5);

        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add New Step", GUILayout.Width(150), GUILayout.Height(25)))
            {
                stepList.arraySize++;
                SerializedProperty newElement = stepList.GetArrayElementAtIndex(stepList.arraySize - 1);
                ResetElementProperties(newElement);
                lastSelectedIndex = stepList.arraySize - 1;
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.Space(5);
        }

        EditorGUILayout.Space(10);

        for (int i = 0; i < stepList.arraySize; i++)
        {
            SerializedProperty step = stepList.GetArrayElementAtIndex(i);

            SerializedProperty dialogueProp = step.FindPropertyRelative("Dialogue");

            SerializedProperty conditionProp = step.FindPropertyRelative("CompletionCondition");
            SerializedProperty eventProp = step.FindPropertyRelative("GameplayEvent");
            SerializedProperty allowedTargetsProp = step.FindPropertyRelative("AllowedTargets");
            SerializedProperty requiredItemProp = step.FindPropertyRelative("RequiredItem");
            SerializedProperty preparationStageProp = step.FindPropertyRelative("PreparationStage");
            SerializedProperty recipeProp = step.FindPropertyRelative("RequiredRecipe");

            SerializedProperty guidanceProp = step.FindPropertyRelative("ShowGuidance");
            SerializedProperty guidanceTypeProp = step.FindPropertyRelative("GuidanceType");
            SerializedProperty guidanceIconProp = step.FindPropertyRelative("GuidanceIcon");

            SerializedProperty spawnOrderRequired = step.FindPropertyRelative("IsOrderSpawnRequired");
            SerializedProperty orderToSpawnProp = step.FindPropertyRelative("OrderToSpawn");

            EditorGUI.indentLevel = 1;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                bool isSelected = (i == lastSelectedIndex);
                GUIStyle boxStyle = isSelected ? EditorStyles.selectionRect : EditorStyles.helpBox;

                using (new EditorGUILayout.VerticalScope(boxStyle))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        string headerLabel = string.IsNullOrEmpty(dialogueProp.stringValue)
                            ? $"Step #{i}"
                            : $"Step #{i}: {dialogueProp.stringValue}";

                        step.isExpanded = EditorGUILayout.Foldout(step.isExpanded, headerLabel, true, EditorStyles.foldoutHeader);

                        // Sıralama Butonları (Up / Down)
                        EditorGUI.BeginDisabledGroup(i == 0);
                        if (GUILayout.Button("▲", GUILayout.Width(22)))
                        {
                            stepList.MoveArrayElement(i, i - 1);
                            serializedObject.ApplyModifiedProperties();
                            GUI.FocusControl(null);
                            return;
                        }
                        EditorGUI.EndDisabledGroup();

                        EditorGUI.BeginDisabledGroup(i == stepList.arraySize - 1);
                        if (GUILayout.Button("▼", GUILayout.Width(22)))
                        {
                            stepList.MoveArrayElement(i, i + 1);
                            serializedObject.ApplyModifiedProperties();
                            GUI.FocusControl(null);
                            return;
                        }
                        EditorGUI.EndDisabledGroup();

                        if (GUILayout.Button("X", GUILayout.Width(22)))
                        {
                            stepList.DeleteArrayElementAtIndex(i);
                            serializedObject.ApplyModifiedProperties();
                            GUI.FocusControl(null);
                            return;
                        }
                    }



                    if (step.isExpanded)
                    {
                        EditorGUILayout.Space(5);
                        EditorGUILayout.PropertyField(dialogueProp);

                        EditorGUILayout.Space(5);
                        EditorGUILayout.PropertyField(conditionProp);

                        if ((TutorialCompletionCondition)conditionProp.enumValueFlag == TutorialCompletionCondition.None ||
                            (TutorialCompletionCondition)conditionProp.enumValueFlag == TutorialCompletionCondition.Tap)
                        {
                            eventProp.enumValueIndex = 0;
                            allowedTargetsProp.arraySize = 0;
                            requiredItemProp.enumValueIndex = 0;
                            preparationStageProp.enumValueIndex = 0;
                            recipeProp.objectReferenceValue = null;
                        }

                        if ((TutorialCompletionCondition)conditionProp.enumValueFlag == TutorialCompletionCondition.GameplayEvent)
                        {
                            EditorGUILayout.PropertyField(eventProp);
                            EditorGUILayout.PropertyField(allowedTargetsProp, true);

                            if ((GameplayEvent)eventProp.enumValueFlag != GameplayEvent.OrderDelivered)
                            {
                                EditorGUILayout.PropertyField(requiredItemProp);
                                EditorGUILayout.PropertyField(preparationStageProp);
                            }

                            else
                            {
                                requiredItemProp.enumValueIndex = 0;
                                preparationStageProp.enumValueIndex = 0;
                                EditorGUILayout.PropertyField(recipeProp);
                            }
                        }

                        EditorGUILayout.Space(5);

                        EditorGUILayout.PropertyField(guidanceProp);
                        if (guidanceProp.boolValue)
                        {
                            EditorGUILayout.PropertyField(guidanceTypeProp);
                            EditorGUILayout.PropertyField(guidanceIconProp);
                        }

                        EditorGUILayout.Space(5);

                        EditorGUILayout.PropertyField(spawnOrderRequired);
                        if (spawnOrderRequired.boolValue)
                        {
                            EditorGUILayout.PropertyField(orderToSpawnProp);
                        }

                        EditorGUILayout.Space(5);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    private void ResetElementProperties(SerializedProperty element)
    {
        SerializedProperty dialogueProp = element.FindPropertyRelative("Dialogue");
        if (dialogueProp != null) dialogueProp.stringValue = string.Empty;

        SerializedProperty conditionProp = element.FindPropertyRelative("CompletionCondition");
        if (conditionProp != null) conditionProp.enumValueIndex = 0;

        SerializedProperty eventProp = element.FindPropertyRelative("GameplayEvent");
        if (eventProp != null) eventProp.enumValueIndex = 0;

        SerializedProperty allowedTargetsProp = element.FindPropertyRelative("AllowedTargets");
        if (allowedTargetsProp != null) allowedTargetsProp.arraySize = 0;

        SerializedProperty requiredItemProp = element.FindPropertyRelative("RequiredItem");
        if (requiredItemProp != null) requiredItemProp.enumValueIndex = 0;

        SerializedProperty preparationStageProp = element.FindPropertyRelative("PreparationStage");
        if (preparationStageProp != null) preparationStageProp.enumValueIndex = 0;

        SerializedProperty recipeProp = element.FindPropertyRelative("RequiredRecipe");
        if (recipeProp != null) recipeProp.objectReferenceValue = null;

        SerializedProperty guidanceProp = element.FindPropertyRelative("ShowGuidance");
        if (guidanceProp != null) guidanceProp.boolValue = false;

        SerializedProperty guidanceTypeProp = element.FindPropertyRelative("GuidanceType");
        if (guidanceTypeProp != null) guidanceTypeProp.enumValueIndex = 0;

        SerializedProperty guidanceIconProp = element.FindPropertyRelative("GuidanceIcon");
        if (guidanceIconProp != null) guidanceIconProp.objectReferenceValue = null;

        SerializedProperty orderRequiredProp = element.FindPropertyRelative("IsOrderSpawnRequired");
        if (orderRequiredProp != null) orderRequiredProp.boolValue = false;

        SerializedProperty requiredOrder = element.FindPropertyRelative("OrderToSpawn");
        if (requiredOrder != null) requiredOrder.objectReferenceValue = null;
    }
}