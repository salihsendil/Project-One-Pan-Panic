using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part Customization SO", menuName = "Scriptable Objects/Customization/New Body Part Customization SO")]
public class BodyPartCustomizationSO : ScriptableObject
{
    public List<CustomizationData> ClothsList = new List<CustomizationData>();
}

[Serializable]
public struct CustomizationData
{
    public BodyPartType BodyPart;
    public string Id;
    public Mesh Mesh;
    public Material BodyColorMaterial;
    public Material FaceMaterial;
    public int Cost;
}