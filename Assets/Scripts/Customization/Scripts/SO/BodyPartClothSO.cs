using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part Cloth SO", menuName = "Scriptable Objects/Customization/New Body Part Cloth SO")]
public class BodyPartClothSO : ScriptableObject
{
    public List<ClothData> Cloths = new List<ClothData>();

}

[Serializable]
public struct ClothData
{
    public BodyPartType BodyPart;
    public string Id;
    public Mesh Mesh;
    public int Cost;
}
