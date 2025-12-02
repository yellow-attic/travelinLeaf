using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectTools : MonoBehaviour
{
    [Header("Buttons Setup")]
    [SerializeField] private  List<Button> buttons = new List<Button>();

    [Header("Scale Settings")]
    public float normalScale = 1f;
    public float selectedScale = 1.2f;

    private Button currentSelected;


    void Start()
    {
        foreach (Button btn in buttons)
        {
            Button capturedBtn = btn;
            btn.onClick.AddListener(() => OnButtonClicked(capturedBtn));
        }
    }

    void OnButtonClicked(Button clickedBtn)
    {
        if (currentSelected == clickedBtn)
        {
            StartCoroutine(ScaleSmooth(clickedBtn.transform, normalScale));
            currentSelected = null;
            return;
        }

        foreach (Button btn in buttons)
        {
            if (btn != clickedBtn)
            {
                StartCoroutine(ScaleSmooth(btn.transform, normalScale));
            }
        }

        StartCoroutine(ScaleSmooth(clickedBtn.transform, selectedScale));
        currentSelected = clickedBtn;
    }

    public void SelectedButton(Button currentButton)
    {
        if (currentSelected == currentButton)
        {
            StartCoroutine(ScaleSmooth(currentButton.transform, normalScale));
            currentSelected = null;
            return;
        }

        foreach (Button btn in buttons)
        {
            if (btn != currentButton)
            {
                StartCoroutine(ScaleSmooth(btn.transform, normalScale));
            }
        }

        StartCoroutine(ScaleSmooth(currentButton.transform, selectedScale));
        currentSelected = currentButton;
    }


    IEnumerator ScaleSmooth(Transform target, float scale)
    {
        Vector3 start = target.localScale;
        Vector3 end = Vector3.one * scale;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 8f;
            target.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }
}