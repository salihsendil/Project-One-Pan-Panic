using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Body Part Catalog", menuName = "Scriptable Objects/Customization/New Body Part Catalog SO")]
public class BodyPartCatalogSO : ScriptableObject
{
    public List<CatalogData> Catalog = new List<CatalogData>();
}

[Serializable]
public struct CatalogData
{
    public BodyPartType BodyPart;
    public BodyPartCustomizationSO PartCloths;
}