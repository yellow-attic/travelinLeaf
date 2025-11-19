using UnityEngine;

public class BreathingEmission : MonoBehaviour
{
    public Renderer targetRenderer;
    public Color emissionColor = Color.white;
    public float speed = 2f;
    public float minIntensity = 0.2f;
    public float maxIntensity = 2f;

    private Material mat;
    private float emissionStrength;

    void Start()
    {
        if (targetRenderer == null)
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