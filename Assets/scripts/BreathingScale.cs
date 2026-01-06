using UnityEngine;

public class BreathingScale : MonoBehaviour
{
    [Header("Breath Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float scaleAmplitude = 0.1f;
    [SerializeField] private float baseScale = 1f;

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        float sin = Mathf.Sin(Time.time * speed);

        float scale = baseScale + sin * scaleAmplitude;

        rect.localScale = Vector3.one * scale;
    }
}