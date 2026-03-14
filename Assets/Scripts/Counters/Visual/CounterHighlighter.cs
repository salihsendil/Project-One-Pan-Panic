using UnityEngine;

public class CounterHighlighter : MonoBehaviour
{
    private Material[] materials = new Material[3];
    [SerializeField] private Color originalEmission = Color.clear;
    [SerializeField] private Color highlightColor = new Color(0.1f, 0.1f, 0.1f, 0.4f);

    private void Awake()
    {
        var renderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }
    }

    public void HighlightObject(bool isOn)
    {
        foreach (var material in materials)
        {
            if (material == null || !material.HasProperty("_EmissionColor"))
            {
                return;
            }

            if (isOn)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", highlightColor);
            }

            if (!isOn)
            {
                material.SetColor("_EmissionColor", originalEmission);
            }
        }
    }
}
