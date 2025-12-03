using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LineMergeAnimation : MonoBehaviour
{
    [SerializeField] private LineRenderer lineA;      // 左边线
    [SerializeField] private LineRenderer lineB;      // 右边线
    [SerializeField] private LineRenderer mergedLine; // 最终合成的一条线

    [SerializeField] private float mergeDurationInner = 1.5f; // 动画时长
    [SerializeField] private float mergeDurationOuter = 1.5f; // 动画时长

    private bool isMerging = false;

    [Header("Glow Settings")]
    [SerializeField] private Color finallineColor;

    Material matA;
    Material matB;
    Color originalColor;

    [SerializeField] private Renderer[] materialsToAnimate;
    [SerializeField] private float colorTransitionDuration = 1f;
    [SerializeField] private Color finalmoleColor;


    void Awake()
    {
        matA = lineA.material;
        matB = lineB.material;

        originalColor = matA.color;   // 假设 A/B 是同色的

        matA = lineA.material;
        matB = lineB.material;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            StartMerge();
        }
    }

    public void StartMerge()
    {
        if (!isMerging)
            StartCoroutine(MergeLinesCoroutine());
    }

    private System.Collections.IEnumerator MergeLinesCoroutine()
    {
        isMerging = true;

        // 假设每条线都是 2 个点：0 = 外侧, 1 = 靠中间
        if (lineA.positionCount < 2 || lineB.positionCount < 2)
        {
            Debug.LogWarning("LineA / LineB 至少需要 2 个点");
            yield break;
        }

        // 记录初始点位置
        Vector3 aOuter = lineA.GetPosition(0);
        Vector3 aInnerStart = lineA.GetPosition(1);

        Vector3 bInnerStart = lineB.GetPosition(0);
        Vector3 bOuter = lineB.GetPosition(1);

        float elapsedTotal = 0f;
        float totalDuration = mergeDurationInner + mergeDurationOuter;


        // -------- 阶段 1：内侧点靠拢 --------
        Vector3 meetPointInner = (aInnerStart + bInnerStart) * 0.5f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / mergeDurationInner;
            float k = Mathf.SmoothStep(0, 1, t);

            // A 的内侧点 → 内侧中点
            lineA.SetPosition(1, Vector3.Lerp(aInnerStart, meetPointInner, k));

            // B 的内侧点 → 内侧中点
            lineB.SetPosition(0, Vector3.Lerp(bInnerStart, meetPointInner, k));

            // 发光动画更新
            elapsedTotal += Time.deltaTime;
            UpdateGlow(elapsedTotal, totalDuration);

            yield return null;
        }

        // -------- 阶段 2：外侧点靠拢 --------
        Vector3 aInnerNow = lineA.GetPosition(1);
        Vector3 bInnerNow = lineB.GetPosition(0);

        Vector3 meetPointOuter = (aOuter + bOuter) * 0.5f;

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / mergeDurationOuter;
            float k = Mathf.SmoothStep(0, 1, t);

            // A 的外侧点 → 外侧中点
            lineA.SetPosition(1, Vector3.Lerp(aInnerNow, meetPointOuter, k));

            // B 的外侧点 → 外侧中点
            lineB.SetPosition(0, Vector3.Lerp(bInnerNow, meetPointOuter, k));

            // 发光动画更新（整段合并过程都在更新）
            elapsedTotal += Time.deltaTime;
            UpdateGlow(elapsedTotal, totalDuration);

            yield return null;
        }

        // ------- 合成最终的一条线 -------
        if (mergedLine != null)
        {
            mergedLine.gameObject.SetActive(true);

            lineA.gameObject.SetActive(false);
            lineB.gameObject.SetActive(false);
        }

        //collider & particle aus
        //alle material green
        //alle line green
        StartCoroutine(FinalColorTransition());

        isMerging = false;
    }


    void UpdateGlow(float elapsed, float totalDuration)
    {
        float t = Mathf.Clamp01(elapsed / totalDuration);

        // 前半段：原色 → 白色
        Color targetColor;
        if (t < 0.5f)
        {
            float k = t / 0.5f;
            targetColor = Color.Lerp(originalColor, Color.white, k);
        }
        // 后半段：白色 → finalColor
        else
        {
            float k = (t - 0.5f) / 0.5f;
            targetColor = Color.Lerp(Color.white, finallineColor, k);
        }

        // 更新两条线的颜色
        lineA.startColor = targetColor;
        lineA.endColor = targetColor;

        lineB.startColor = targetColor;
        lineB.endColor = targetColor;

        // 更新材质颜色（若 LineRenderer 使用 sharedMaterial）
        matA.color = targetColor;
        matB.color = targetColor;
    }

    void SetFinalNonGlowState()
    {
        matA.color = finallineColor;
        matB.color = finallineColor;
    }

    IEnumerator FinalColorTransition()
    {
        float t = 0f;

        // 记录所有材质的原始颜色
        Color[] originalColors = new Color[materialsToAnimate.Length];
        for (int i = 0; i < materialsToAnimate.Length; i++)
        {
            originalColors[i] = materialsToAnimate[i].material.color;
        }

        // ---------- 阶段 1：原色 → 白色 ----------
        while (t < 1f)
        {
            t += Time.deltaTime / (colorTransitionDuration * 0.5f);
            float k = Mathf.SmoothStep(0, 1, t);

            for (int i = 0; i < materialsToAnimate.Length; i++)
            {
                materialsToAnimate[i].material.color = Color.Lerp(originalColors[i], Color.white, k);
            }

            yield return null;
        }

        // ---------- 阶段 2：白色 → finalColor ----------
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / (colorTransitionDuration * 0.5f);
            float k = Mathf.SmoothStep(0, 1, t);

            for (int i = 0; i < materialsToAnimate.Length; i++)
            {
                materialsToAnimate[i].material.color = Color.Lerp(Color.white, finalmoleColor, k);
            }

            yield return null;
        }
    }
}