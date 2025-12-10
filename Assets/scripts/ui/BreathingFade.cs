using UnityEngine;
using UnityEngine.UI;

public class BreathingFade : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minAlpha = 0.2f;
    [SerializeField] private float maxAlpha = 1f;

    private Color baseColor;

    void Start()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        baseColor = targetImage.color;
    }

    void Update()
    {
        float sinValue = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f;

        float alpha = Mathf.Lerp(minAlpha, maxAlpha, sinValue);

        Color c = baseColor;
        c.a = alpha;
        targetImage.color = c;
    }
}