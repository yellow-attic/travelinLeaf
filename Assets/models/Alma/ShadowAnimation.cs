using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    [Header("Breath Animation")]
    [SerializeField] float _amplitude = 0.2f; // the height of the animation
    [SerializeField] float _frequency = 2f; // the speed of the animation

    private Vector3 _localscale;

    [Header("Scale Animation")]
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float animationTime = 1f;

    private float startTime;



    private  void Start()
    {
        startTime = 0.0f;
        _localscale = transform.localScale;
    }

    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float timePassed = Time.time - startTime;
        float animationPercent = Mathf.Clamp01(timePassed / animationTime);

        if(animationPercent < 1)
        {
            float scale = scaleCurve.Evaluate(animationPercent);
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            float _scale = _amplitude * Mathf.Sin(Time.time * _frequency);
            _scale = -_scale;

            transform.localScale = _localscale + new Vector3(_scale, _scale, _scale);
        }
    }

}