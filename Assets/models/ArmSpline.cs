using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class ArmSpline : MonoBehaviour {

    [SerializeField] private int _pointsPerSegment = 8;
    [SerializeField] private Transform[] _handleTransforms;

    [SerializeField] private DynamicPoint[] _dynamicPoints;

    [System.Serializable] struct DynamicPoint {
        public Transform transform;
        public float influence;
    }

    private Vector3 prevEndpoint;

    private void Start() {
        prevEndpoint = _handleTransforms.Last().localPosition;    
    }

    private void Update() {
        _updateDynamicPoints();
        updateLines();
    }

    private void _updateDynamicPoints() {
        Vector3 newEndpoint = _handleTransforms.Last().localPosition;
        Vector3 delta = newEndpoint - prevEndpoint;

        foreach (DynamicPoint dp in _dynamicPoints) {
            dp.transform.position += delta * dp.influence;
        }

        prevEndpoint = newEndpoint;
    }

    private Vector3[] collectHandles() {
        Vector3[] handles = new Vector3[_handleTransforms.Length];
        
        for (int i = 0; i < _handleTransforms.Length; i++) {
            handles[i] = _handleTransforms[i].position;
        }

        return handles;
    }

    private Vector3[] applyGeneralBezierInterpolation() {
        Vector3[] handles = collectHandles();

        if (handles == null || handles.Length < 2)
            return null;

        List<Vector3> result = new List<Vector3>();

        Vector3 start = handles[0];
        Vector3 end = handles[handles.Length - 1];

        int steps = _pointsPerSegment * (handles.Length - 1);

        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;

            // De Casteljau-style blending
            List<Vector3> temp = new List<Vector3>(handles);

            for (int k = handles.Length - 1; k > 0; k--)
            {
                for (int j = 0; j < k; j++)
                {
                    temp[j] = Vector3.Lerp(temp[j], temp[j + 1], t);
                }
            }

            result.Add(temp[0]);
        }

        return result.ToArray();
    }

    private Vector3[] applyInterpolation() {
        Vector3[] handles = collectHandles();

        if (handles == null || handles.Length < 2)
            return null;

        List<Vector3> result = new List<Vector3>();

        for (int i = 0; i < handles.Length - 1; i++)
        {
            Vector3 p0 = i == 0
                ? handles[i]
                : (handles[i - 1] + handles[i]) * 0.5f;

            Vector3 p1 = handles[i];

            Vector3 p2 = (i == handles.Length - 2)
                ? handles[i + 1]
                : (handles[i] + handles[i + 1]) * 0.5f;

            Vector3 p3 = (i + 2 < handles.Length)
                ? handles[i + 2]
                : handles[i + 1];

            // for cubic bezier
            Vector3 c1 = p1 + (p2 - p0) / 6f;
            Vector3 c2 = p2 - (p3 - p1) / 6f;

            for (int j = 0; j < _pointsPerSegment; j++)
            {
                float t = j / (float)_pointsPerSegment;
                //result.Add(QuadraticBezier(p0, p1, p2, t));
                //result.Add(CatmullRom(p0, p1, p2, p3, t));
                result.Add(CubicBezier(p1, c1, c2, p2, t));
            }
        }

        // add final handle explicitly
        result.Add(handles[handles.Length - 1]);

        return result.ToArray();
    }

    [ContextMenu("Update Lines")]
    private void updateLines() {
        Vector3[] points = applyGeneralBezierInterpolation();

        // apply points to LineRenderer
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = points.Length;
        lr.SetPositions(points);
    }

    static private Vector3 QuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t) {
        float u = 1f - t;
        return u * u * a + 2f * u * t * b + t * t * c;
    }

    static private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }

    static private Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        return
            uuu * p0 +
            3f * uu * t * p1 +
            3f * u * tt * p2 +
            ttt * p3;
    }
}
