using UnityEngine;

public class BreathingEmission : MonoBehaviour
{
    private Renderer targetRenderer;

    [SerializeField] private Color emissionColor = Color.white;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 2f;

    private Material mat;
    private float emissionStrength;

    void Start()
    {
        targetRenderer = GetComponent<Renderer>();

        mat = targetRenderer.material;
        mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;

        emissionStrength = Mathf.Lerp(minIntensity, maxIntensity, t);

        mat.SetColor("_EmissionColor", emissionColor * emissionStrength);
    }
}