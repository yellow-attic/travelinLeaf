using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float idleTime = 5f;

    private float _scalevalue;
    private float startTime;

    private void Start()
    {
        _scalevalue = transform.localScale.x;

        InvokeRepeating("DebugMethod", 0f, idleTime);
    }

    public void resetAnimation()
    {
        startTime = 0.0f;
        transform.localScale = Vector3.zero;
    }



    void DebugMethod()
    {
        startTime = Time.time;
    }

    [ContextMenu("scale")]
    private void Update()
    {
        float timePassed = Time.time - startTime;
        float animationPercent = Mathf.Clamp01(timePassed / animationTime);
        float scale = scaleCurve.Evaluate(animationPercent);
        transform.localScale = new Vector3(_scalevalue, scale * _scalevalue, _scalevalue);
    }

}
