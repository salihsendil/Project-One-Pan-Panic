using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterPartSelection
{
    public CharacterPartType characterPart;
    public EquipmentSO equipment;
}

[CreateAssetMenu(fileName = "New Customization Set", menuName = "Scriptable Object/Character Customization/New Customization Set")]
public class CustomizationSetSO : ScriptableObject
{
    [SerializeField] private List<CharacterPartSelection> partSelections = new();

    public List<CharacterPartSelection> PartSelections => partSelections;
}
