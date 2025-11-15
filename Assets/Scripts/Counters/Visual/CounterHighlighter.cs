using UnityEngine;

public class CounterHighlighter : MonoBehaviour
{
    private Renderer _renderer;
    private Material _material;
    [SerializeField] private Color originalEmission = Color.clear;
    [SerializeField] public Color highlightColor = new Color(0.1f, 0.1f, 0.1f, 0.4f);

    private void Awake()
    {
        TryGetComponent(out _renderer);
        _material = _renderer.material;
    }

    public void HighlightObject(bool isOn)
    {
        if (_material == null || !_material.HasProperty("_EmissionColor"))
        {
            return;
        }

        if (isOn)
        {
            _material.EnableKeyword("_EMISSION");
            _material.SetColor("_EmissionColor", highlightColor);
        }

        if (!isOn)
        {
            _material.SetColor("_EmissionColor", originalEmission);
        }

    }
}
