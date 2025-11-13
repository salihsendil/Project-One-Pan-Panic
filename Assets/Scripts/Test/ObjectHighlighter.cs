using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    private Renderer _renderer;
    private Material _material;
    [SerializeField] private Color originalEmission = Color.clear;
    [SerializeField] public Color highlightColor;

    private void Awake()
    {
        TryGetComponent(out _renderer);
    }

    void Start()
    {
        _material = _renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HighlightObject(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            HighlightObject(false);
        }
    }

    private void HighlightObject(bool isOn)
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
