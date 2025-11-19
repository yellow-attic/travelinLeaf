using UnityEngine;
using UnityEngine.UI;

public class SliderColorGradient : MonoBehaviour
{
    [SerializeField] private Color green;
    [SerializeField] private Color red;
    [SerializeField] private Color yellow;

    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;


    void Update()
    {
        float value = slider.value;

        if (value >= 0.6f)
        {
            fillImage.color = green;
        }
        else if (value >= 0.4f)
        {
            float t = Mathf.InverseLerp(0.6f, 0.4f, value);
            fillImage.color = Color.Lerp(green, yellow, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0.4f, 0f, value);
            fillImage.color = Color.Lerp(yellow, red, t);
        }
    }
}