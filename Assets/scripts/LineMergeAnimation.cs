using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class LineMergeAnimation : MonoBehaviour {

    [SerializeField] private Renderer[] materialsToAnimate;

    private bool _isMerging = false;

    [SerializeField, FormerlySerializedAs("curve")] private AnimationCurve _connectAnimationCurve;

    [Header("Color Settings")]
    [SerializeField, FormerlySerializedAs("finallineColor")] private Color _finalLineColor;
    [SerializeField, FormerlySerializedAs("finalmoleColor")] private Color _finalMoleColor;

    [ContextMenu("Test Merge Animation")]
    public void play() {
        if (!_isMerging)
            StartCoroutine(_doMergeAnimation());
    }

    private IEnumerator _doMergeAnimation() {
        _isMerging = true;

        BrokenStation brokenStation = GetComponent<BrokenStation>();

        // first find center, 0.5 way between lineA start and lineB start
        Vector3 target = brokenStation.mergedLine.center();
        Vector3 lineAFrom = brokenStation.lineA.end.position;
        Vector3 lineBFrom = brokenStation.lineB.end.position;

        float time = 0.0f;
        const float Duration = 1.9f;

        Color start = brokenStation.lineA.renderer().material.color;

        while (time <= Duration) {
            time += Time.deltaTime;
            float nrm = Mathf.Clamp01(time / Duration);

            // apply back easing for nicer animation
            float easedNrm = _connectAnimationCurve.Evaluate(nrm); // Raumkapsel.Ease.BackIn(nrm);
            brokenStation.lineA.end.position = Vector3.LerpUnclamped(lineAFrom, target, easedNrm);
            brokenStation.lineB.end.position = Vector3.LerpUnclamped(lineBFrom, target, easedNrm);

            brokenStation.lineA.GetComponent<Line>().applyPositions();
            brokenStation.lineB.GetComponent<Line>().applyPositions();

            // also animate color of line materials
            brokenStation.lineA.renderer().material.color = Color.Lerp(start, _finalLineColor, nrm);
            brokenStation.lineB.renderer().material.color = Color.Lerp(start, _finalLineColor, nrm);

            yield return new WaitForEndOfFrame();
        }


        yield return new WaitForSeconds(0.033f);

        // this also enables the merged line particles, set the edge orientation correctly
        brokenStation.setRepairedConnection();

        // set the particle position to match line center
        ParticleSystem ps = brokenStation.mergedLine.GetComponentInChildren<ParticleSystem>();
        ps.gameObject.transform.position = target;

        // only thing left is to interpolate remaining molecule parts to final color
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

        // just some more waiting time TODO: remove?
        yield return new WaitForSeconds(2.5f);
    }
}