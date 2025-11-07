using UnityEngine;

[CreateAssetMenu(fileName = "New UIActionButtonSO", menuName = "Scriptable Object/New UIActionButtonSO")]
public class UIActionButtonSO : ScriptableObject
{
    public UIButtonActionType actionType;
    public ScenesEnum scene;
    public Sprite toggleOnSprite;
    public Sprite toggleOffSprite;
}
