using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class LineMergeAnimation : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("lineA")] private LineRenderer _lineA;      // 左边线
    [SerializeField, FormerlySerializedAs("lineB")] private LineRenderer _lineB;      // 右边线
    [SerializeField, FormerlySerializedAs("mergedLine")] private LineRenderer _mergedLine; // 最终合成的一条线
    [SerializeField] private Renderer[] materialsToAnimate;

    private bool isMerging = false;

    [Header("Glow Settings")]
    [SerializeField, FormerlySerializedAs("finallineColor")] private Color _finalLineColor;
    [SerializeField, FormerlySerializedAs("finalmoleColor")] private Color _finalMoleColor;

    void Start() {
        // start disabled
        _mergedLine.gameObject.SetActive(false);

        // start broken parts enabled
        _lineA.gameObject.SetActive(true);
        _lineB.gameObject.SetActive(true);
    }

    [ContextMenu("Test Merge Animation")]
    public void play() {
        if (!isMerging)
            StartCoroutine(_doMergeAnimation());
    }

    [ContextMenu("Set Particle Orientation")]
    private void setMergedParticleOrientation() {
        Vector3 target = (_lineA.GetPosition(0) + _lineB.GetPosition(1)) * 0.5f;

        ParticleSystem ps = _mergedLine.GetComponentInChildren<ParticleSystem>();
        ps.gameObject.transform.position = target;

        //var shape = ps.shape;
        //Vector3 direction = (_lineA.GetPosition(0) - _lineB.GetPosition(1)).normalized;
        //shape.rotation = Quaternion.LookRotation(direction, Vector3.down).eulerAngles;
    }

    private IEnumerator _doMergeAnimation() {
        isMerging = true;
        // smoothly interpolate line ends towards center point

        // first find center, 0.5 way between lineA start and lineB start
        Vector3 target = (_lineA.GetPosition(0) + _lineB.GetPosition(1)) * 0.5f;
        Vector3 lineAFrom = _lineA.GetPosition(1);
        Vector3 lineBFrom = _lineB.GetPosition(0);

        float time = 0.0f;
        const float Duration = 1.3f;

        Color start = _lineA.material.color;

        while (time <= Duration) {
            time += Time.deltaTime;
            float nrm = Mathf.Clamp01(time / Duration);

            // apply back easing for nicer animation
            // https://easings.net/#easeInBack
            _lineA.SetPosition(1, Vector3.LerpUnclamped(lineAFrom, target, Raumkapsel.Ease.BackIn(nrm)));
            _lineB.SetPosition(0, Vector3.LerpUnclamped(lineBFrom, target, Raumkapsel.Ease.BackIn(nrm)));

            // also animate color of line materials
            _lineA.material.color = Color.Lerp(start, _finalLineColor, nrm);
            _lineB.material.color = Color.Lerp(start, _finalLineColor, nrm);

            yield return new WaitForEndOfFrame();
        }


        yield return new WaitForSeconds(0.033f);

        // complete connect by diabling separate lines and enabling merged line
        Debug.Assert(_mergedLine != null);

        // this also enables the merged line particles, set the edge orientation correctly
        _mergedLine.gameObject.SetActive(true);

        // set the particle position to match line center
        setMergedParticleOrientation();

        _lineA.gameObject.SetActive(false);
        _lineB.gameObject.SetActive(false);

        // now interpolate remaining molecule parts to final color
        Color[] originalColors = new Color[materialsToAnimate.Length];
        for (int i = 0; i < materialsToAnimate.Length; i++) {
            originalColors[i] = materialsToAnimate[i].material.color;
        }

        time = 0.0f;
        const float MoleculeColorDuration = 2.5f;
        while (time <= MoleculeColorDuration) {
            time += Time.deltaTime;
            float nrm = Mathf.Clamp01(time / MoleculeColorDuration);

            for (int i = 0; i < materialsToAnimate.Length; i++) {
                materialsToAnimate[i].material.color = Color.Lerp(originalColors[i], _finalMoleColor, Raumkapsel.Ease.CubicInOut(nrm));
            }

            yield return new WaitForEndOfFrame();
        }
    }
}