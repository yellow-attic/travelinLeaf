using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnetTime : MonoBehaviour
{
    public float duration = 4f;

    private Image targetImage;


    private void Start()
    {
        targetImage = GetComponent<Image>();
        StartCoroutine(FillOverTime());
    }

    IEnumerator FillOverTime()
    {
        if (targetImage == null)
        {
            yield break;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            targetImage.fillAmount = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        targetImage.fillAmount = 0f;
    }
}