using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weltenzimmer {

    public class LineDrag : MonoBehaviour {
        // Blending & Noise happen in local space
        // drag distortion in global space. all internal 

        // base shape
        const int COUNT = 32;
        private Vector3[] _shapeCircle = new Vector3[COUNT];

        private Vector3 _prevScale; // alma transform

        [SerializeField] private Transform _bodyTransform;

        [SerializeField] private float _noiseAmount = 0.1f;
        [SerializeField] private float _noiseFreq = 4.0f;
        [SerializeField] private float _noiseSize = 1.0f;

        [SerializeField] private float _dragAmount = 0.4f;

        [Header("Talk Animation")]
        [SerializeField] private GameObject _voiceAnimated;


        void Start() {
            GetComponentInChildren<LineRenderer>().positionCount = COUNT;

            for (int i = 0; i < COUNT; i++) {
                float angle = (i / (float)COUNT) * (Mathf.PI * 2);
                float r = 1.8f;
                Vector3 vec = new Vector3(Mathf.Sin(angle) * r, Mathf.Cos(angle) * r, 0);
                
                _shapeCircle[i] = vec;
            }

            transform.localEulerAngles = new Vector3(-70.0f, 0f, 0f);
        }

        private float _noiseFromAudioVolume() {
            if (_voiceAnimated != null && _voiceAnimated.activeSelf)
            {
                return 1.0f;
            }
            else
            {
                return 0.0f;
            } 
            //foreach (AudioSource source in _audioSourceParent.GetComponentsInChildren<AudioSource>())
            //{
            //    if (source.isPlaying)
            //    {
            //        float[] samples = new float[source.clip.samples * source.clip.channels];
            //        source.clip.GetData(samples, 0);
            //        float sampleVol = samples[source.timeSamples];
            //        return Mathf.Clamp01(Mathf.Abs(samples[source.timeSamples]));
            //    }
            //}
        }

        void Update() {
            // new array for current positions
            Vector3[] points = new Vector3[_shapeCircle.Length];
            for (int i = 0; i < points.Length; i++)
                points[i] = _shapeCircle[i];

            // add drag distortion - assumes no local space rotations...

            Vector3 drag = (_bodyTransform.localScale - _prevScale);
            for (int i = 0; i < points.Length; i++) {
                // apply uneven amount of drag to all points
                float alongShape = Mathf.Abs((i / (float)points.Length * 2.0f) - 1.0f);
                float waves = Mathf.Abs(Mathf.Sin(i * 0.4f)); // waves
                float amount = (alongShape + waves) * _dragAmount;
                //Vector3 offset = new Vector3(Mathf.Clamp(drag.x * -amount, -drag.x, drag.x), Mathf.Clamp(drag.y * -amount, -drag.y, drag.y), Mathf.Clamp(drag.z * -amount, -drag.z, drag.z));

                points[i] = points[i] + drag * -amount;
            }

            // at this point clamp positions to collision circle positions, to avoid collision with head
            const float RADIUS = 0.9f;
            for (int i = 0; i < points.Length; i++) {
                if (points[i].magnitude < RADIUS) {
                    //points[i] = _shapeCircle[i];
                    // TODO: handle special case where distance is bigger than radius, but intersects with sphere...
                }
            }

            // store prev
            _prevScale = Vector3.Lerp(_prevScale, _bodyTransform.localScale, Time.deltaTime * 10.0f);

            // add noise
            float at = Time.time * _noiseFreq;
            float noiseAmount = _noiseAmount * _noiseFromAudioVolume();
            for (int i = 0; i < points.Length; i++) {
                Vector3 p = points[i] * _noiseSize;
                Vector3 noise = new Vector3(Mathf.PerlinNoise(p.x, p.y + at), Mathf.PerlinNoise(p.x, p.y + at + 50.0f), Mathf.PerlinNoise(p.x + at, p.y)) - new Vector3(0.5f, 0.5f, 0.5f);
                points[i] = points[i] + (noise * noiseAmount);
            }

            _applyPointsToLines(points);
        }

        private void _applyPointsToLines(Vector3[] points) {
            GetComponentInChildren<LineRenderer>().SetPositions(points);
        }

        [ContextMenu("Set Circle Shape")]
        private void _setCirclePoints() {
            GetComponentInChildren<LineRenderer>().SetPositions(_shapeCircle);
        }
    }
}