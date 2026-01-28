using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConnetTime : MonoBehaviour
{
    [SerializeField] private float duration = 4f;

    private Image targetImage;

    [NonReorderable] public bool isintime;

    [SerializeField] private ToolConnect toolconnect;
    [SerializeField] private HandConnectManager handconnect;


    private void OnEnable()
    {
        targetImage = GetComponent<Image>();
        StartCoroutine(FillOverTime());

        targetImage.fillAmount = 1f;
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
            isintime = true;
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            targetImage.fillAmount = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        targetImage.fillAmount = 0f;
        isintime = false;
        closePanel();
    }

    private void closePanel()
    {
        gameObject.SetActive(false);

        //player backward
        toolconnect.ConnectFailed();
        handconnect.ConnectpanelReset();
    }

    public void ConnectPanelClose()
    {
        gameObject.SetActive(false);
        //toolconnect.ConnectFailed();
        handconnect.ConnectpanelReset();
    }
}