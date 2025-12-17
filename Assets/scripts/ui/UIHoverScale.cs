using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Target")]
    private RectTransform target;
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float pressScale = 0.9f;

    [Header("Animation")]
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 originalScale;
    private Coroutine scaleRoutine;

    private bool isHovering = false;
    private bool isPressing = false;


    private void Start()
    {
        target = GetComponent<RectTransform>();

        originalScale = target.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (!isPressing)
            AnimateTo(originalScale * hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (!isPressing)
            AnimateTo(originalScale);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        AnimateTo(originalScale * pressScale);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;

        Vector3 targetScale = isHovering
            ? originalScale * hoverScale
            : originalScale;

        AnimateTo(targetScale);
    }

    void AnimateTo(Vector3 targetScale)
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(ScaleRoutine(targetScale));
    }

    void StartScale(Vector3 targetScale)
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(ScaleRoutine(targetScale));
    }

    IEnumerator ScaleRoutine(Vector3 targetScale)
    {
        Vector3 startScale = target.localScale;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // UI 用 unscaled
            float t = time / duration;
            float curveValue = scaleCurve.Evaluate(t);

            target.localScale = Vector3.LerpUnclamped(
                startScale,
                targetScale,
                curveValue
            );

            yield return null;
        }

        target.localScale = targetScale;
    }
}